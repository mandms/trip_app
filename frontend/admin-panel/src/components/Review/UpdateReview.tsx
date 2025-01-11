import React, { useEffect, useState } from 'react';
import {
  TextField,
  Button,
  DialogContent,
  Dialog,
  DialogTitle,
  DialogActions,
  Box,
  Rating,
} from '@mui/material';
import { useMutation, useQueryClient } from 'react-query';
import ReviewService from '../../api/api.review';
import { IUpdateReview } from '../../types/types.review';

interface IUpdateReviewProps {
  close: () => void;
  open: boolean;
  updatedReview: IUpdateReview;
}

const UpdateReview: React.FC<IUpdateReviewProps> = ({
  close,
  open,
  updatedReview,
}) => {
  const [review, setReview] = useState<{ text: string; rate: number }>({
    text: '',
    rate: 0,
  });

  useEffect(() => {
    setReview({
      text: updatedReview.text,
      rate: updatedReview.rate,
    });
  }, [updatedReview]);

  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: async () => ReviewService.update(updatedReview.id, review),
    onSuccess: async () => {
      await queryClient.invalidateQueries('reviews'); // Обновляем список отзывов
      close();
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
      <DialogTitle>Изменить отзыв</DialogTitle>
      <DialogContent>
        <form onSubmit={handleSubmit}>
          <Box sx={{ p: 1 }}>
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
            <Button
              type="submit"
              variant="contained"
              color="primary"
              fullWidth
              sx={{ marginTop: 2 }}
            >
              Изменить отзыв
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

export default UpdateReview;
export type { IUpdateReviewProps };
