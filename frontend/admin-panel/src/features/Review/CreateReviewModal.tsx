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
  Rating,
} from '@mui/material';
import { useMutation, useQueryClient } from 'react-query';
import ReviewService from '../../api/api.review';
import { parseErrorMessage } from '../../utils/errorMessageParser';

interface ICreateReviewModalProps {
  close: () => void;
  open: boolean;
}

interface ICreateReview {
  routeId: string;
  text: string;
  rate: number;
}

const CreateReviewModal: React.FC<ICreateReviewModalProps> = ({
  close,
  open,
}) => {
  const [review, setReview] = useState<ICreateReview>({
    routeId: '',
    text: '',
    rate: 0,
  });
  const [error, setError] = useState<string | null>(null);

  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: async () => {
      const routeId = review.routeId;
      const reviewBody = {
        text: review.text,
        rate: review.rate,
      };
      await ReviewService.create(routeId, reviewBody);
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries('reviews'); // Обновляем список отзывов
      close();
    },
    onError: (error: unknown) => {
      const errorMessage = parseErrorMessage(error);
      setError(errorMessage);
    },
  });

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => {
    const { name, value } = e.target;
    setReview((prev) => ({ ...prev, [name]: value }));
  };

  const handleRateChange = (
    event: React.SyntheticEvent,
    value: number | null,
  ) => {
    setReview((prev) => ({ ...prev, rate: value || 0 }));
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
      <DialogTitle>Создать отзыв</DialogTitle>
      <DialogContent>
        <form onSubmit={handleSubmit}>
          <Box sx={{ p: 1 }}>
            <TextField
              label="Route ID"
              variant="outlined"
              fullWidth
              name="routeId"
              value={review.routeId}
              onChange={handleChange}
              required
              sx={{ mb: 2 }}
            />
            <TextField
              label="Текст отзыва"
              variant="outlined"
              fullWidth
              name="text"
              value={review.text}
              onChange={handleChange}
              multiline
              rows={4}
              required
              sx={{ mb: 2 }}
            />
            <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
              <Rating
                name="rate"
                value={review.rate}
                onChange={handleRateChange}
                max={5}
              />
              <Box sx={{ ml: 2 }}>{review.rate || 0} / 5</Box>
            </Box>
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
              Создать отзыв
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

export default CreateReviewModal;
