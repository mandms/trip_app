import React, { useState } from 'react';
import {
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Button,
  Typography,
  Card,
  CardContent,
  Box,
  Avatar,
  CircularProgress,
  Grid2,
} from '@mui/material';
import { useQuery } from 'react-query';
import RouteService from '../../../api/api.route';
import DetailRouteMap from './DetailRouteMap';
import { LatLngLiteral } from 'leaflet';
import { ICoordinates } from '../../../types/types.coordinates';
import ImagesSlider from '../../../components/Slider/ImagesSlider';

interface DetailRouteModalProps {
  open: boolean;
  routeId: string;
  onClose: () => void;
}

const DetailRouteModal: React.FC<DetailRouteModalProps> = ({
  open,
  routeId,
  onClose,
}) => {
  const [scopeCoords, setScopeCoords] = useState<LatLngLiteral | null>(null);

  const { data: route, isLoading } = useQuery(
    ['route', routeId],
    () => RouteService.getById(routeId),
    {
      onSuccess: (route) =>
        setScopeCoords({
          lat: route!.locations[0].coordinates.latitude,
          lng: route!.locations[0].coordinates.longitude,
        }),
      enabled: open,
    },
  );

  if (isLoading) {
    return (
      <Dialog open={open} onClose={onClose} maxWidth="md" fullWidth>
        <DialogTitle>Loading...</DialogTitle>
        <DialogContent>
          <CircularProgress />
        </DialogContent>
      </Dialog>
    );
  }

  const onLocationClick = (coords: ICoordinates) => {
    setScopeCoords({ lat: coords.latitude, lng: coords.longitude });
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="md" fullWidth>
      {route && route.locations && scopeCoords && (
        <DetailRouteMap
          locations={route.locations}
          scopeMapCoords={scopeCoords}
        />
      )}
      <DialogTitle>Название маршрута: {route?.name}</DialogTitle>
      <DialogContent>
        <Typography variant="h6">Описание:</Typography>
        <Typography>{route?.description}</Typography>

        <Box mt={2}>
          <Typography variant="h6">Пользователь:</Typography>
          <Box display="flex" alignItems="center">
            <Avatar src={route?.user.avatar} alt={route?.user.username} />
            <Box ml={2}>
              <Typography variant="body1">{route?.user.username}</Typography>
            </Box>
          </Box>
        </Box>

        <Box mt={2}>
          <Typography variant="h6">
            Продолжительность: {route?.duration} часов
          </Typography>
        </Box>

        <Box mt={2}>
          <Typography mb={1} variant="h6">
            Тэги:
          </Typography>
          <Grid2 container spacing={1}>
            {route?.tags.map((tag) => (
              <Grid2 key={tag.id}>
                <Button variant="outlined" size="small">
                  {tag.name}
                </Button>
              </Grid2>
            ))}
          </Grid2>
        </Box>

        <Box mt={2}>
          <Typography mb={1} variant="h6">
            Локации:
          </Typography>
          <Grid2 container spacing={2}>
            {route?.locations.map((location, idx) => (
              <Grid2 key={location.id}>
                <Card>
                  <CardContent>
                    <Button
                      variant={'outlined'}
                      fullWidth={true}
                      onClick={() => onLocationClick(location.coordinates)}
                    >
                      Показать на карте
                    </Button>
                    <Typography mt={2} variant="h6">
                      Локация {idx + 1}: {location.name}
                    </Typography>
                    <Typography mt={2} variant="body1">
                      Описание: {location.description}
                    </Typography>
                    <Box mt={2}>
                      <Typography variant="body1" color="textSecondary">
                        Изображения:
                      </Typography>
                      <ImagesSlider images={location.images} />
                    </Box>
                  </CardContent>
                </Card>
              </Grid2>
            ))}
          </Grid2>
        </Box>
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose} color="primary">
          Закрыть
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default DetailRouteModal;
