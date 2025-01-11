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
} from '@mui/material';
import { useMutation, useQueryClient } from 'react-query';
import UserService from '../../api/api.user';
import { parseErrorMessage } from '../../ErrorParser';
import { ICreateUser } from '../../types/types.user';

interface ICreateUserProps {
  close: () => void;
  open: boolean;
}

const CreateUser: React.FC<ICreateUserProps> = ({ close, open }) => {
  const [user, setUser] = useState<ICreateUser>({
    email: '',
    username: '',
    password: '',
  });
  const [error, setError] = useState<string | null>(null);

  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: async () => UserService.create(user),
    onSuccess: async () => {
      await queryClient.invalidateQueries('users');
      close();
    },
    onError: (error: unknown) => {
      const errorMessage = parseErrorMessage(error);
      setError(errorMessage);
    },
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setUser((prev) => ({ ...prev, [name]: value }));
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
      <DialogTitle>Создать пользователя</DialogTitle>
      <DialogContent>
        <form onSubmit={handleSubmit}>
          <Box sx={{ p: 1 }}>
            <TextField
              label="Email"
              variant="outlined"
              fullWidth
              name="email"
              value={user.email}
              onChange={handleChange}
              required
              sx={{ mb: 2 }}
            />
            <TextField
              label="Имя пользователя"
              variant="outlined"
              fullWidth
              name="username"
              value={user.username}
              onChange={handleChange}
              required
              sx={{ mb: 2 }}
            />
            <TextField
              label="Пароль"
              variant="outlined"
              fullWidth
              type="password"
              name="password"
              value={user.password}
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
              Создать пользователя
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

export default CreateUser;
export type { ICreateUserProps };
