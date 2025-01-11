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
} from '@mui/material';
import React, { ChangeEvent, useState } from 'react';
import { ICreateLocation } from '../../../types/types.location';
import { useMutation, useQueryClient } from 'react-query';
import { parseErrorMessage } from '../../../utils/errorMessageParser';
import LocationService from '../../../api/api.location';
import { toBase64 } from '../../../utils/fileUtils';
import { ICreateImage } from '../../../types/types.image';

interface CreateRouteLocationModalProps {
  close: () => void;
  open: boolean;
  routeId: string;
}

const InitialState: ICreateLocation = {
  coordinates: {
    latitude: 0,
    longitude: 0,
  },
  description: '',
  images: [],
  name: '',
};

const CreateRouteLocationModal: React.FC<CreateRouteLocationModalProps> = ({
  close,
  open,
  routeId,
}) => {
  const [location, setLocation] = useState<ICreateLocation>(InitialState);
  const [error, setError] = useState<string | null>(null);

  const queryClient = useQueryClient();

  const addLocationMutation = useMutation({
    mutationFn: async () => {
      location.images = location.images.map((data) => ({
        ...data,
        image: data.image.replace(/^data:image\/\w+;base64,/, ''),
      }));
      await LocationService.create(routeId, location);
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries('updateRoute');
      handleClose();
    },
    onError: (error: unknown) => {
      const errorMessage = parseErrorMessage(error);
      setError(errorMessage);
    },
  });

  const handleLocationChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setLocation((prevState) => ({ ...prevState, [name]: value }));
  };

  const handleCoordinatesChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    const newCoords = {
      ...location.coordinates,
      [name]: value,
    };
    setLocation({ ...location, coordinates: newCoords });
  };

  const handleImageUpload = async (e: React.ChangeEvent<HTMLInputElement>) => {
    const files = e.target.files;
    if (files) {
      const base64Images = await Promise.all(
        Array.from(files).map((file) => toBase64(file)),
      );
      const newImages = [
        ...base64Images.map((base64): ICreateImage => {
          return { image: base64 };
        }),
      ];
      setLocation((prevState) => ({
        ...prevState,
        images: [...newImages],
      }));
    }
  };

  const handleClose = () => {
    close();
    setLocation(InitialState);
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    addLocationMutation.mutate();
  };

  return (
    <Dialog
      open={open}
      onClose={handleClose}
      maxWidth="sm"
      fullWidth
      aria-modal={true}
    >
      <DialogTitle>Создать локацию</DialogTitle>
      <DialogContent>
        <form onSubmit={handleSubmit}>
          <Box
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
                    handleLocationChange(e)
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
                    handleLocationChange(e)
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
                    handleCoordinatesChange(e)
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
                    handleCoordinatesChange(e)
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
                      onChange={(e) => handleImageUpload(e)}
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
          {error && (
            <Alert severity="error" sx={{ marginTop: 2 }}>
              {error}
            </Alert>
          )}
          <Box sx={{ p: 1 }}>
            <Button
              type="submit"
              variant="contained"
              color="primary"
              fullWidth
              sx={{ marginTop: 2 }}
            >
              Создать локацию
            </Button>
          </Box>
        </form>
      </DialogContent>
      <DialogActions>
        <Button type="submit" onClick={handleClose} color="secondary">
          Закрыть
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default CreateRouteLocationModal;
