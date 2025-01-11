import {
  Alert,
  AppBar,
  Box,
  Button,
  Grid2,
  TextField,
  Typography,
} from '@mui/material';
import React, { ChangeEvent, useState } from 'react';
import { ILocation, IUpdateLocation } from '../../../types/types.location';
import { useMutation, useQueryClient } from 'react-query';
import LocationService from '../../../api/api.location';
import { IDetailRoute } from '../../../types/types.route';
import { parseErrorMessage } from '../../../ErrorParser';
import ImageManager from '../../Image/ImageManager';
import CreateRouteLocation from './CreateRouteLocation';

interface IRouteLocationsProps {
  route: IDetailRoute;
  setRoute: React.Dispatch<React.SetStateAction<IDetailRoute>>;
  setSuccess: () => void;
}

const RouteLocations: React.FC<IRouteLocationsProps> = ({
  route,
  setRoute,
  setSuccess,
}) => {
  const locations = route.locations;

  const [open, setOpen] = useState(false);
  const [errors, setErrors] = useState<
    { message: string; locationId: string }[]
  >([]);
  const queryClient = useQueryClient();

  const mutationDelete = useMutation(LocationService.delete, {
    onSuccess: async () => {
      setSuccess();
      await queryClient.invalidateQueries('updateRoute');
    },
  });

  const mutationUpdate = useMutation({
    mutationFn: async ({
      locationId,
      location,
    }: {
      locationId: string;
      location: IUpdateLocation;
    }) => await LocationService.update(locationId, location),
    onSuccess: async () => {
      setSuccess();
      await queryClient.invalidateQueries('updateRoute');
    },
    onError: (error: unknown, params) => {
      const errorMessage = parseErrorMessage(error);
      setErrors((prevState) => [
        ...prevState,
        {
          message: errorMessage,
          locationId: params.locationId,
        },
      ]);
    },
  });

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

  const handleDeleteLocation = (
    event: React.MouseEvent<HTMLButtonElement, MouseEvent>,
    id: string,
  ) => {
    mutationDelete.mutate(id);
  };

  const handleLocationUpdate = (location: ILocation) => {
    const locationId = location.id;
    mutationUpdate.mutate({ locationId, location });
  };

  const updateError = (locationId: string) => {
    const error = errors.find((error) => error.locationId === locationId);
    if (!error) return;
    return error;
  };

  const handleClose = () => {
    setOpen((prevState) => !prevState);
  };

  const handleOpen = () => {
    setOpen((prevState) => !prevState);
  };

  return (
    <Grid2 size={{ xs: 12 }}>
      <AppBar
        position="sticky"
        color={'transparent'}
        elevation={0}
        sx={{ width: '120px', marginLeft: 'auto' }}
      >
        <Button
          variant="contained"
          color="success"
          size="medium"
          onClick={handleOpen}
        >
          Добавить
        </Button>
        <CreateRouteLocation
          routeId={route.id}
          close={handleClose}
          open={open}
        />
      </AppBar>
      <Typography variant="h6">Локации (Кол-во: {locations.length})</Typography>
      {locations.map((location, index) => (
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
                onChange={(e: ChangeEvent<HTMLInputElement>) => {
                  handleLocationChange(e, index);
                }}
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
                onChange={(e: ChangeEvent<HTMLInputElement>) => {
                  handleLocationChange(e, index);
                }}
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
                onChange={(e: ChangeEvent<HTMLInputElement>) => {
                  handleCoordinatesChange(e, index);
                }}
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
                onChange={(e: ChangeEvent<HTMLInputElement>) => {
                  handleCoordinatesChange(e, index);
                }}
                required
                type="number"
              />
            </Grid2>
            {updateError(location.id)?.message && (
              <Alert severity="error" sx={{ marginTop: 2 }}>
                {updateError(location.id)?.message}
              </Alert>
            )}
            <Grid2 size={{ xs: 6 }}>
              <Button
                onClick={() => handleLocationUpdate(location)}
                fullWidth
                variant={'contained'}
              >
                Обновить данные о локации
              </Button>
            </Grid2>
            <Grid2 size={{ xs: 6 }}>
              <Button
                onClick={(e) => handleDeleteLocation(e, location.id)}
                fullWidth
                variant={'contained'}
                color="secondary"
              >
                Удалить локацию
              </Button>
            </Grid2>
            <Box
              sx={{
                marginBottom: 2,
                padding: 2,
                border: '1px solid #ccc',
                borderRadius: 2,
                width: '100%',
              }}
            >
              <ImageManager
                images={location.images}
                id={location.id}
                removeImages={LocationService.removeImages}
                addImages={LocationService.addImages}
                invalidateQueries={() =>
                  queryClient.invalidateQueries('updateRoute')
                }
              />
            </Box>
          </Grid2>
        </Box>
      ))}
    </Grid2>
  );
};

export default RouteLocations;
