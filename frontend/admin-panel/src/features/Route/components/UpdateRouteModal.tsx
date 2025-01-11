import React, { useState } from 'react';
import {
  Alert,
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Fade,
  Grid2,
  Tab,
  Tabs,
} from '@mui/material';
import { useMutation, useQuery, useQueryClient } from 'react-query';
import RouteService from '../../../api/api.route';
import TagService from '../../../api/api.tag';
import RouteInfoTab from '../tabs/RouteInfoTab';
import RouteLocationsTab from '../tabs/RouteLocationsTab';
import { IDetailRoute, IUpdateRoute } from '../../../types/types.route';
import { IRouteTag } from '../../../types/types.tag';
import { parseErrorMessage } from '../../../utils/errorMessageParser';
import RouteTagsTab from '../tabs/RouteTagsTab';

interface IUpdateRouteModalProps {
  close: () => void;
  open: boolean;
  routeId: string;
}

interface TabPanelProps {
  children?: React.ReactNode;
  value: number;
  index: number;
}

const TabPanel: React.FC<TabPanelProps> = ({ children, value, index }) => {
  return <Box>{value === index && children}</Box>;
};

const InitialRoute = {
  description: '',
  duration: 0,
  id: '',
  locations: [],
  name: '',
  state: 0,
  status: 0,
  tags: [],
  user: {
    id: '',
    username: '',
    avatar: '',
  },
};

const UpdateRouteModal: React.FC<IUpdateRouteModalProps> = ({
  close,
  routeId,
  open,
}) => {
  const [route, setRoute] = useState<IDetailRoute>(InitialRoute);
  const [tagList, setTagList] = useState<IRouteTag[]>([]);
  const [selectedTagList, setSelectedTagList] = useState<IRouteTag[]>([]);
  const [tabValue, setTabValue] = useState(1);
  const [successAlert, setSuccessAlert] = useState(false);
  const [error = { route: '', locations: '', tags: '' }, setError] = useState({
    route: '',
    locations: '',
    tags: '',
  });

  useQuery(['updateRoute', routeId], () => RouteService.getById(routeId), {
    onSuccess: (route) => {
      setRoute(route);
      setSelectedTagList(route.tags);
    },
    enabled: open,
  });

  useQuery(['tags'], () => TagService.getAll(), {
    onSuccess: (tags) => {
      const selectedTags = new Set(selectedTagList.map((tag) => tag.id));
      const filteredTags = tags.data.filter((tag) => !selectedTags.has(tag.id));
      setTagList(filteredTags);
    },
    enabled: open && route.id !== '',
  });

  const queryClient = useQueryClient();

  const successUpdate = () => {
    setSuccessAlert(true);
    setTimeout(() => {
      setSuccessAlert(false);
    }, 3000);
  };

  const mutationRouteInfo = useMutation({
    mutationFn: async (route: IUpdateRoute) =>
      await RouteService.update(route.id, route),
    onSuccess: async () => {
      await queryClient.invalidateQueries('routes');
      successUpdate();
    },
    onError: (error: unknown) => {
      const errorMessage = parseErrorMessage(error);
      setError((prevState) => ({
        ...prevState,
        route: errorMessage,
      }));
    },
  });

  const mutationAddRouteTags = useMutation({
    mutationFn: async (tags: IRouteTag[]) => {
      await RouteService.addTags(
        route.id,
        tags.map((tag) => tag.id),
      );
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries('routes');
      successUpdate();
    },
    onError: (error: unknown) => {
      const errorMessage = parseErrorMessage(error);
      setError((prevState) => ({
        ...prevState,
        route: errorMessage,
      }));
    },
  });

  const mutationDeleteRouteTags = useMutation({
    mutationFn: async (tags: IRouteTag[]) => {
      await RouteService.removeTags(
        route.id,
        tags.map((tag) => tag.id),
      );
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries('routes');
      successUpdate();
    },
    onError: (error: unknown) => {
      const errorMessage = parseErrorMessage(error);
      setError((prevState) => ({
        ...prevState,
        route: errorMessage,
      }));
    },
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setRoute({
      ...route,
      [name]: value,
    });
  };

  const handleStatusToggle = (e: React.ChangeEvent<HTMLInputElement>) => {
    const isChecked = e.target.checked;
    setRoute((prevRoute) => {
      const newRoute = {
        ...prevRoute,
        status: isChecked ? 0 : 1,
      };
      return newRoute;
    });
  };

  const handleRouteInfoSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    const routeInfo: IUpdateRoute = {
      description: route.description,
      duration: route.duration,
      id: `${route.id}`,
      name: route.name,
      status: route.status,
    };
    mutationRouteInfo.mutate(routeInfo);
  };

  const handleRouteChangeTagsSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    const selectedTags = new Set(route.tags.map((tag) => tag.id));
    const newTags = selectedTagList.filter(
      (selectedTag) => !selectedTags.has(selectedTag.id),
    );
    const deleteTags = tagList.filter((selectedTag) =>
      selectedTags.has(selectedTag.id),
    );

    if (newTags.length) {
      mutationAddRouteTags.mutate(newTags);
    }

    if (deleteTags.length) {
      mutationDeleteRouteTags.mutate(deleteTags);
    }
  };

  const handleClose = () => {
    close();
    setRoute(InitialRoute);
    setSelectedTagList([]);
    setTagList([]);
    setTabValue(1);
  };

  return (
    <Dialog
      open={open}
      onClose={handleClose}
      maxWidth="md"
      fullWidth
      aria-modal={true}
    >
      <DialogTitle>Обновить маршрут</DialogTitle>
      <Fade in={successAlert} unmountOnExit={true} timeout={1100}>
        <Alert severity="success">Успешно обновлено!</Alert>
      </Fade>
      <Tabs
        value={tabValue}
        onChange={(e, newValue) => setTabValue(newValue)}
        aria-label="basic tabs example"
      >
        <Tab label="Что вы хотите изменить?" disabled />
        <Tab label="Данные о маршруте" />
        <Tab label="Тэги" />
        <Tab label="Локации" />
      </Tabs>
      <DialogContent sx={{ height: '400px' }}>
        <TabPanel value={tabValue} index={1}>
          <form onSubmit={handleRouteInfoSubmit}>
            <Grid2 container spacing={2}>
              <RouteInfoTab
                route={route}
                handleChange={handleChange}
                handleStatusToggle={handleStatusToggle}
              />
            </Grid2>
            {!!error.route.length && (
              <Alert severity="error" sx={{ marginTop: 2 }}>
                {error.route}
              </Alert>
            )}
            <Button
              type="submit"
              variant="contained"
              color="primary"
              fullWidth
              sx={{ marginTop: 2 }}
            >
              Обновить основные данные
            </Button>
          </form>
        </TabPanel>
        <TabPanel value={tabValue} index={2}>
          <RouteTagsTab
            tagList={tagList}
            setTagList={setTagList}
            handleRouteChangeTagsSubmit={handleRouteChangeTagsSubmit}
            setSelectedTagList={setSelectedTagList}
            selectedTagList={selectedTagList}
          />
        </TabPanel>
        <TabPanel value={tabValue} index={3}>
          <RouteLocationsTab
            setSuccess={successUpdate}
            route={route}
            setRoute={setRoute}
          />
        </TabPanel>
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose} color="secondary">
          Закрыть
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default UpdateRouteModal;
