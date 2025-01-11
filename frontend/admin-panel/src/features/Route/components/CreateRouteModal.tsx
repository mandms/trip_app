import React, { ChangeEvent, useEffect, useState } from 'react';
import {
  Alert,
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Grid2,
  TextField,
  Typography,
} from '@mui/material';
import { useMutation, useQueryClient } from 'react-query';
import RouteService from '../../../api/api.route';
import CreateRouteMap from './CreateRouteMap';
import { toBase64 } from '../../../utils/fileUtils';
import { parseErrorMessage } from '../../../utils/errorMessageParser';
import TagService from '../../../api/api.tag';
import RouteTags from '../../../components/RouteTags/RouteTags';
import RouteInfoTab from '../tabs/RouteInfoTab';
import { ICreateRoute } from '../../../types/types.route';
import { IRouteTag } from '../../../types/types.tag';
import { ICreateImage } from '../../../types/types.image';
import { removeBase64Prefix } from '../../../utils/removeBase64Prefix';

interface ICreateRouteModalProps {
  close: () => void;
  open: boolean;
}

const initialState = {
  name: '',
  description: '',
  duration: 0,
  tags: [],
  locations: [],
  status: 1,
};

const CreateRouteModal: React.FC<ICreateRouteModalProps> = ({
  open,
  close,
}) => {
  const [route, setRoute] = useState<ICreateRoute>(initialState);
  const [error, setError] = useState<string | null>(null);
  const [tagList, setTagList] = useState<IRouteTag[]>([]);
  const [selectedTagList, setSelectedTagList] = useState<IRouteTag[]>([]);
  const queryClient = useQueryClient();

  useEffect(() => {
    const fetchTagsHandler = async () => {
      try {
        const tags = await TagService.getAll(1, 100);
        setTagList(tags.data);
      } catch (error) {
        const errorMessage = parseErrorMessage(error);
        setError(errorMessage);
      }
    };

    if (open) {
      fetchTagsHandler().catch(() => setError('Неопознанная ошибка'));
    }
  }, [open]);

  const mutation = useMutation({
    mutationFn: async () => {
      route.locations = route.locations.map((location) => {
        return {
          ...location,
          images: location.images.map((data) => {
            return { image: removeBase64Prefix(data.image) };
          }),
        };
      });
      route.tags = selectedTagList.map((tag) => +tag.id);
      await RouteService.create(route);
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries('routes');
      clear();
    },
    onError: (error: unknown) => {
      const errorMessage = parseErrorMessage(error);
      setError(errorMessage);
    },
  });

  const clear = () => {
    close();
    setRoute(initialState);
    setError(null);
    setSelectedTagList([]);
    setTagList([]);
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setRoute({
      ...route,
      [name]: value,
    });
  };

  const handleLocationChange = (
    e: React.ChangeEvent<HTMLInputElement>,
    index: number,
  ) => {
    const { name, value } = e.target;
    const updatedLocations = [...route.locations];
    updatedLocations[index] = { ...updatedLocations[index], [name]: value };
    setRoute({ ...route, locations: updatedLocations });
  };

  const handleCoordinatesChange = (
    e: React.ChangeEvent<HTMLInputElement>,
    index: number,
  ) => {
    const { name, value } = e.target;
    const updatedLocations = [...route.locations];
    updatedLocations[index].coordinates = {
      ...updatedLocations[index].coordinates,
      [name]: value,
    };
    setRoute({ ...route, locations: updatedLocations });
  };

  const handleAddLocation = (latitude: number, longitude: number) => {
    setRoute({
      ...route,
      locations: [
        ...route.locations,
        {
          name: '',
          description: '',
          coordinates: { latitude: latitude, longitude: longitude },
          images: [],
        },
      ],
    });
  };

  const handleStatusToggle = (e: React.ChangeEvent<HTMLInputElement>) => {
    const isChecked = e.target.checked;
    setRoute((prevRoute) => {
      const updatedRoute = {
        ...prevRoute,
        status: isChecked ? 0 : 1,
      };
      return updatedRoute;
    });
  };

  const errorHandler = () => {
    setError(null);
    !selectedTagList.length &&
      setError('Ошибка тэгов: Выберите хотябы один тэг. ');
    !route.locations.length &&
      setError(
        (prev) =>
          `${prev ? prev : ''} Ошибка локаций: Добавьте хотябы две локации. `,
      );
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    errorHandler();
    !error && mutation.mutate();
  };

  const handleImageUpload = async (
    locationIdx: number,
    e: React.ChangeEvent<HTMLInputElement>,
  ) => {
    const files = e.target.files;
    if (files) {
      const base64Images = await Promise.all(
        Array.from(files).map((file) => toBase64(file)),
      );
      const newLocations = route.locations.map((location, idx) => {
        if (idx === locationIdx) {
          const images = [
            ...base64Images.map((base64): ICreateImage => {
              return { image: base64 };
            }),
          ];
          location.images = [];
          location.images.push(...images);
        }
        return location;
      });
      setRoute((prev) => ({
        ...prev,
        locations: [...newLocations],
      }));
    }
  };

  return (
    <Dialog
      open={open}
      onClose={clear}
      maxWidth="md"
      fullWidth
      aria-modal={true}
    >
      <DialogTitle>Добавить новый маршрут</DialogTitle>
      <DialogContent>
        <form onSubmit={handleSubmit}>
          <CreateRouteMap addLocation={handleAddLocation} />
          <Grid2 container spacing={2} sx={{ marginTop: 2 }}>
            <RouteInfoTab
              route={route}
              handleChange={handleChange}
              handleStatusToggle={handleStatusToggle}
            />
            <RouteTags
              selectedTagList={selectedTagList}
              setSelectedTagList={setSelectedTagList}
              setTagList={setTagList}
              tagList={tagList}
            />
            <Grid2 size={{ xs: 12 }}>
              <Typography variant="h6">Локации</Typography>
              {route.locations.map((location, index) => (
                <Box
                  key={index}
                  sx={{
                    marginBottom: 2,
                    padding: 2,
                    border: '1px solid #ccc',
                    borderRadius: 2,
                  }}
                >
                  <Grid2 container spacing={2}>
                    <Grid2 size={{ xs: 12 }}>
                      <TextField
                        label="Название локации"
                        variant="outlined"
                        fullWidth
                        name="name"
                        value={location.name}
                        onChange={(e: ChangeEvent<HTMLInputElement>) =>
                          handleLocationChange(e, index)
                        }
                        required
                      />
                    </Grid2>
                    <Grid2 size={{ xs: 12 }}>
                      <TextField
                        label="Описание локации"
                        variant="outlined"
                        fullWidth
                        name="description"
                        value={location.description}
                        onChange={(e: ChangeEvent<HTMLInputElement>) =>
                          handleLocationChange(e, index)
                        }
                        required
                        multiline
                        rows={4}
                      />
                    </Grid2>
                    <Grid2 size={{ xs: 6 }}>
                      <TextField
                        label="Широта"
                        variant="outlined"
                        fullWidth
                        name="latitude"
                        value={location.coordinates.latitude}
                        onChange={(e: ChangeEvent<HTMLInputElement>) =>
                          handleCoordinatesChange(e, index)
                        }
                        required
                        type="number"
                      />
                    </Grid2>
                    <Grid2 size={{ xs: 6 }}>
                      <TextField
                        label="Долгота"
                        variant="outlined"
                        fullWidth
                        name="longitude"
                        value={location.coordinates.longitude}
                        onChange={(e: ChangeEvent<HTMLInputElement>) =>
                          handleCoordinatesChange(e, index)
                        }
                        required
                        type="number"
                      />
                    </Grid2>
                    <Grid2 size={{ xs: 12 }}>
                      <Box sx={{ mt: 2 }}>
                        <Button variant="contained" component="label">
                          Загрузить изображения
                          <input
                            type="file"
                            accept="image/*"
                            multiple
                            hidden
                            onChange={(e) => handleImageUpload(index, e)}
                          />
                        </Button>
                      </Box>
                    </Grid2>
                    {location.images.length > 0 && (
                      <Box sx={{ mt: 2 }}>
                        {location.images.map((img, index) => (
                          <img
                            key={index}
                            src={img.image}
                            alt={`upload-${index}`}
                            style={{
                              maxWidth: '100%',
                              maxHeight: '150px',
                              margin: '10px',
                            }}
                          />
                        ))}
                      </Box>
                    )}
                  </Grid2>
                </Box>
              ))}
            </Grid2>
          </Grid2>
          {error && (
            <Alert severity="error" sx={{ marginTop: 2 }}>
              {error}
            </Alert>
          )}
          <Button
            type="submit"
            variant="contained"
            color="primary"
            fullWidth
            sx={{ marginTop: 2 }}
          >
            Добавить маршрут
          </Button>
        </form>
      </DialogContent>
      <DialogActions>
        <Button onClick={clear} color="secondary">
          Закрыть
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default CreateRouteModal;
