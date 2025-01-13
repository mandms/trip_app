import {
  Avatar,
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Typography,
} from '@mui/material';
import React, { useState } from 'react';
import { useQuery } from 'react-query';
import MomentService from '../../api/api.moment';
import { LatLngExpression, LatLngLiteral } from 'leaflet';
import ImagesSlider from '../../components/Slider/ImagesSlider';
import MomentMap from './MomentMap';

interface IDetailMomentModalProps {
  momentId: string;
  close: () => void;
  open: boolean;
}

const DetailMomentModal: React.FC<IDetailMomentModalProps> = ({
  momentId,
  open,
  close,
}) => {
  const [coords, setCoords] = useState<LatLngLiteral>();

  const { data: moment, isLoading } = useQuery(
    ['moment', momentId],
    () => MomentService.getById(momentId),
    {
      onSuccess: (moment) =>
        setCoords({
          lat: moment.coordinates.latitude,
          lng: moment.coordinates.longitude,
        }),
      enabled: open,
    },
  );

  const handleClose = () => {
    close();
    setCoords(undefined);
  };

  return (
    <Dialog open={open} onClose={handleClose} maxWidth="md" fullWidth>
      <DialogTitle>Момент</DialogTitle>
      <DialogContent>
        <Box mb={2} sx={{ height: '400px' }}>
          <MomentMap location={{ coords: coords, image: moment?.images[0] }} />
        </Box>
        <Box sx={{ height: '200px' }}>
          <ImagesSlider images={moment?.images} />
        </Box>
        <Typography mt={2} variant="h6">
          Описание:
        </Typography>
        <Typography>{moment?.description}</Typography>
        <Typography mt={2}>
          Статус: {moment?.status ? 'Скрыт' : 'Показан'}
        </Typography>
        <Box mt={2}>
          <Typography variant="h6">Пользователь:</Typography>
          <Box display="flex" alignItems="center">
            <Avatar src={moment?.user.avatar} alt={moment?.user.username} />
            <Box ml={2}>
              <Typography variant="body1">{moment?.user.username}</Typography>
            </Box>
          </Box>
        </Box>
        <Typography mt={2}>Дата создания: {moment?.createdAt}</Typography>
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose} color="primary">
          Закрыть
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default DetailMomentModal;
