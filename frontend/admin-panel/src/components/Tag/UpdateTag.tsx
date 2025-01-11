import React, { useEffect, useState } from 'react';
import {
  TextField,
  Button,
  DialogContent,
  Dialog,
  DialogTitle,
  DialogActions,
  Box,
  InputLabel,
  Select,
  MenuItem,
  FormControl,
  Alert,
} from '@mui/material';
import { useMutation, useQuery, useQueryClient } from 'react-query';
import TagService from '../../api/api.tag';
import CategoryService from '../../api/api.category';
import { parseErrorMessage } from '../../ErrorParser';
import { ITag } from '../../types/types.tag';

interface IUpdateTagProps {
  close: () => void;
  open: boolean;
  updatedTag: ITag;
}

const UpdateTag: React.FC<IUpdateTagProps> = ({ close, open, updatedTag }) => {
  const [tag, setTag] = useState<string>('');
  const [categoryId, setCategoryId] = React.useState(updatedTag.category.id);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    setTag(updatedTag.name);
  }, [updatedTag]);

  const queryClient = useQueryClient();

  const { data: categories } = useQuery(
    ['categories'],
    () => CategoryService.getAll(1, 100),
    {
      keepPreviousData: true,
      onError: (error: unknown) => {
        const errorMessage = parseErrorMessage(error);
        setError(errorMessage);
      },
    },
  );

  const mutation = useMutation({
    mutationFn: async () => TagService.update(updatedTag.id, tag, categoryId),
    onSuccess: async () => {
      await queryClient.invalidateQueries('tags');
      close();
    },
    onError: (error: unknown) => {
      const errorMessage = parseErrorMessage(error);
      setError(errorMessage);
    },
  });

  const handleChange = (tag: string) => {
    setTag(tag);
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
      <DialogTitle>Изменить тэг</DialogTitle>
      <DialogContent>
        <form onSubmit={handleSubmit}>
          <Box sx={{ p: 1 }}>
            <FormControl required sx={{ width: '100%', marginBottom: 2 }}>
              <InputLabel id="category-label">Категория</InputLabel>
              <Select
                labelId="category-label"
                value={categoryId}
                label="Категория *"
                onChange={(e) => setCategoryId(e.target.value)}
              >
                {categories?.data.map(
                  (category: { id: string; name: string }) => (
                    <MenuItem key={category.id} value={category.id}>
                      {category.name}
                    </MenuItem>
                  ),
                )}
              </Select>
            </FormControl>
            <TextField
              label="Тэг"
              variant="outlined"
              fullWidth
              name="text"
              value={tag}
              onChange={(e) => handleChange(e.target.value)}
              multiline
              rows={4}
              required
              sx={{ mb: 2 }}
            />
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
              Изменить тэг
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

export default UpdateTag;
export type { IUpdateTagProps };
