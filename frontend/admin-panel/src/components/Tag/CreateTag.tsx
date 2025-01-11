import React, { useState } from 'react';
import {
  TextField,
  Button,
  DialogContent,
  Dialog,
  DialogTitle,
  DialogActions,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Box,
  SelectChangeEvent,
  Alert,
} from '@mui/material';
import { useMutation, useQuery, useQueryClient } from 'react-query';
import TagService from '../../api/api.tag';
import CategoryService from '../../api/api.category';
import { parseErrorMessage } from '../../ErrorParser';

interface ICreateTagProps {
  close: () => void;
  open: boolean;
}

const CreateTag: React.FC<ICreateTagProps> = (props: ICreateTagProps) => {
  const { close, open } = props;
  const [tag, setTag] = useState<string>('');
  const [categoryId, setCategoryId] = React.useState('');
  const [error, setError] = useState<string | null>(null);

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

  // Mutation for creating the route
  const mutation = useMutation({
    mutationFn: async () => TagService.create(categoryId, tag),
    onSuccess: async () => {
      await queryClient.invalidateQueries('tags');
    },
    onError: (error: unknown) => {
      const errorMessage = parseErrorMessage(error);
      setError(errorMessage);
    },
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setTag(e.target.value);
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    mutation.mutate();
    close();
  };

  const handleSelectCategory = (event: SelectChangeEvent) => {
    setCategoryId(event.target.value);
  };

  return (
    <Dialog
      open={open}
      onClose={close}
      maxWidth="md"
      fullWidth
      aria-modal={true}
    >
      <DialogTitle>Добавить новый тэг</DialogTitle>
      <DialogContent>
        <form onSubmit={handleSubmit}>
          <Box sx={{ p: 1 }}>
            <FormControl required sx={{ width: '100%', marginBottom: 2 }}>
              <InputLabel id="category-label">Категория</InputLabel>
              <Select
                labelId="category-label"
                value={categoryId}
                label="Категория *"
                onChange={handleSelectCategory}
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
              label="Название тэга"
              variant="outlined"
              fullWidth
              name="name"
              value={tag}
              onChange={handleChange}
              required
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
              Добавить тэг
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

export default CreateTag;
export type { ICreateTagProps };
