import React, { useState } from 'react';
import {
  TextField,
  Button,
  DialogContent,
  Dialog,
  DialogTitle,
  DialogActions,
  Box,
  Alert,
  Switch,
  FormControlLabel,
} from '@mui/material';
import { useMutation, useQueryClient } from 'react-query';
import MomentService from '../../api/api.moment';
import { parseErrorMessage } from '../../ErrorParser';
import CreateMomentMap from './CreateMomentMap';
import { toBase64 } from '../../utils/fileUtils';
import { ICreateMoment } from '../../types/types.moment';

interface ICreateMomentProps {
  close: () => void;
  open: boolean;
}

const CreateMoment: React.FC<ICreateMomentProps> = ({ close, open }) => {
  const initialState = {
    description: '',
    coordinates: {
      latitude: 0,
      longitude: 0,
    },
    status: 0,
    images: [],
  };
  const [moment, setMoment] = useState<ICreateMoment>(initialState);
  const [error, setError] = useState<string | null>(null);

  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: async () => {
      moment.images = moment.images.map((data) => ({
        ...data,
        image: data.image.replace(/^data:image\/\w+;base64,/, ''),
      }));
      await MomentService.create(moment);
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries('moments');
      setMoment(initialState);
      setError(null);
      close();
    },
    onError: (error: unknown) => {
      const errorMessage = parseErrorMessage(error);
      setError(errorMessage);
    },
  });

  const handleAddLocation = (latitude: number, longitude: number) => {
    setMoment({
      ...moment,
      coordinates: { latitude: latitude, longitude: longitude },
    });
  };

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => {
    const { name, value } = e.target;
    setMoment((prev) => ({ ...prev, [name]: value }));
  };

  const handleStatusToggle = (e: React.ChangeEvent<HTMLInputElement>) => {
    setMoment((prev) => ({ ...prev, status: e.target.checked ? 1 : 0 }));
  };

  const handleImageUpload = async (e: React.ChangeEvent<HTMLInputElement>) => {
    const files = e.target.files;
    if (files) {
      const base64Images = await Promise.all(
        Array.from(files).map((file) => toBase64(file)),
      );
      setMoment((prev) => ({
        ...prev,
        images: base64Images.map((base64) => ({
          image: base64,
        })),
      }));
    }
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    mutation.mutate();
  };

  return (
    <Dialog
      open={open}
      onClose={close}
      maxWidth="sm"
      fullWidth
      aria-modal={true}
    >
      <DialogTitle>Создать момент</DialogTitle>
      <DialogContent>
        <form onSubmit={handleSubmit}>
          <Box sx={{ p: 1 }}>
            <CreateMomentMap addLocation={handleAddLocation} />
            <TextField
              label="Описание момента"
              variant="outlined"
              fullWidth
              name="description"
              value={moment.description}
              onChange={handleChange}
              multiline
              rows={4}
              required
              sx={{ mb: 2, mt: 2 }}
            />
            <TextField
              label="Широта (latitude)"
              variant="outlined"
              fullWidth
              name="latitude"
              type="number"
              value={moment.coordinates.latitude}
              onChange={handleChange}
              disabled={true}
              sx={{ mb: 2 }}
            />
            <TextField
              label="Долгота (longitude)"
              variant="outlined"
              fullWidth
              name="longitude"
              type="number"
              value={moment.coordinates.longitude}
              onChange={handleChange}
              disabled={true}
              sx={{ mb: 2 }}
            />
            <FormControlLabel
              control={
                <Switch
                  checked={!!moment.status}
                  onChange={handleStatusToggle}
                  name="status"
                  color="primary"
                />
              }
              label="Скрыть от других пользователей"
            />
            <Box sx={{ mt: 2 }}>
              <Button variant="contained" component="label">
                Загрузить изображения
                <input
                  type="file"
                  accept="image/*"
                  multiple
                  hidden
                  onChange={handleImageUpload}
                />
              </Button>
            </Box>
            {moment.images.length > 0 && (
              <Box sx={{ mt: 2 }}>
                {moment.images.map((img, index) => (
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
              Создать момент
            </Button>
          </Box>
        </form>
      </DialogContent>
      <DialogActions>
        <Button onClick={close} color="secondary">
          Закрыть
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default CreateMoment;
export type { ICreateMomentProps };
