import React, { useState } from 'react';
import {
  Alert,
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Switch,
  TextField,
  Typography,
} from '@mui/material';
import {
  QueryClient,
  useMutation,
  useQuery,
  useQueryClient,
} from 'react-query';
import { IMoment, IUpdateMoment } from '../../types/types.moment';
import MomentService from '../../api/api.moment';
import ImageManager from '../../components/Image/ImageManager';
import { parseErrorMessage } from '../../utils/errorMessageParser';

interface IUpdateMomentModalProps {
  close: () => void;
  open: boolean;
  momentId: string;
}

const useMomentData = (
  momentId: string,
  open: boolean,
  setError: React.Dispatch<React.SetStateAction<string | null>>,
) => {
  const [moment, setMoment] = useState<IMoment | null>(null);

  useQuery(['momentDetail', momentId], () => MomentService.getById(momentId), {
    onSuccess: setMoment,
    onError: (error: unknown) => {
      const errorMessage = parseErrorMessage(error);
      setError(errorMessage);
    },
    enabled: open,
  });

  return { moment, setMoment, setError };
};

const useUpdateMoment = (
  moment: IUpdateMoment | null,
  momentId: string,
  handleClose: () => void,
  queryClient: QueryClient,
  setError: React.Dispatch<React.SetStateAction<string | null>>,
) => {
  return useMutation({
    mutationFn: async () => {
      if (moment) {
        const momentData: IUpdateMoment = {
          coordinates: moment.coordinates,
          description: moment.description,
          status: moment.status,
        };
        await MomentService.update(momentId, momentData);
      }
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries('moments');
      handleClose();
    },
    onError: (error: unknown) => {
      const errorMessage = parseErrorMessage(error);
      setError(errorMessage);
    },
  });
};

const handleStatusToggle = (
  e: React.ChangeEvent<HTMLInputElement>,
  setMoment: React.Dispatch<React.SetStateAction<IMoment | null>>,
) => {
  setMoment((prev) => {
    if (!prev) return prev;
    return { ...prev, status: e.target.checked ? 1 : 0 };
  });
};

const UpdateMomentModal: React.FC<IUpdateMomentModalProps> = ({
  close,
  open,
  momentId,
}) => {
  const queryClient = useQueryClient();
  const [error, setError] = useState<string | null>(null);
  const handleClose = () => {
    setMoment(null);
    setError(null);
    close();
  };
  const { moment, setMoment } = useMomentData(momentId, open, setError);
  const mutation = useUpdateMoment(
    moment,
    momentId,
    handleClose,
    queryClient,
    setError,
  );

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    mutation.mutate();
  };

  const invalidateQueries = async () => {
    await queryClient.invalidateQueries('momentDetail');
  };

  const handleFieldChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => {
    const { name, value } = e.target;
    setMoment((prev) => {
      if (!prev) return prev;

      if (name === 'latitude' || name === 'longitude') {
        return {
          ...prev,
          coordinates: {
            ...prev.coordinates,
            [name]: parseFloat(value),
          },
        };
      }

      return {
        ...prev,
        [name]: value,
      };
    });
  };

  return (
    <Dialog
      open={open}
      onClose={handleClose}
      maxWidth="sm"
      fullWidth
      aria-modal={true}
    >
      <DialogTitle>Изменить момент</DialogTitle>
      <DialogContent>
        {moment ? (
          <form onSubmit={handleSubmit}>
            <Box sx={{ p: 1 }}>
              <TextField
                label="Описание"
                variant="outlined"
                fullWidth
                name="description"
                value={moment.description}
                onChange={(e) => handleFieldChange(e)}
                multiline
                rows={4}
                required
                sx={{ mb: 2 }}
              />

              <Box sx={{ display: 'flex', gap: 2, mb: 2 }}>
                <TextField
                  label="Широта"
                  type="number"
                  name="latitude"
                  value={moment.coordinates.latitude}
                  onChange={handleFieldChange}
                  fullWidth
                  required
                />
                <TextField
                  label="Долгота"
                  type="number"
                  name="longitude"
                  value={moment.coordinates.longitude}
                  onChange={handleFieldChange}
                  fullWidth
                  required
                />
              </Box>

              <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                <Switch
                  checked={moment.status === 1}
                  onChange={(e) => handleStatusToggle(e, setMoment)}
                  color="primary"
                />
                <Typography sx={{ ml: 1 }}>
                  {moment.status === 1 ? 'Скрыть' : 'Показать'}
                </Typography>
              </Box>

              {error && (
                <Alert severity="error" sx={{ marginTop: 2 }}>
                  {error}
                </Alert>
              )}

              <ImageManager
                images={moment.images}
                id={moment.id}
                addImages={async (id, files) => {
                  await MomentService.addImages(id, files);
                }}
                removeImages={async (id, imageId) => {
                  await MomentService.removeImages(id, imageId);
                }}
                invalidateQueries={invalidateQueries}
              />

              <Button
                type="submit"
                variant="contained"
                color="primary"
                fullWidth
                sx={{ marginTop: 2 }}
              >
                Изменить момент
              </Button>
            </Box>
          </form>
        ) : (
          <Typography>Загрузка данных...</Typography>
        )}
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose} color="secondary">
          Закрыть
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default UpdateMomentModal;
