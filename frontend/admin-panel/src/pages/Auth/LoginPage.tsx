import React, { useContext, useState } from 'react';
import { Button, TextField, Box, Typography } from '@mui/material';
import { useMutation } from 'react-query';
import UserService from '../../api/api.user';
import { Navigate, useNavigate } from 'react-router-dom';
import { CurrentUserContext } from '../../stores/CurrentUserContext';

type LoginError = { Error: string[] };

const LoginPage = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState<LoginError | null>(null);

  const navigate = useNavigate();

  const { setUser } = useContext(CurrentUserContext);

  const mutation = useMutation(UserService.login, {
    onSuccess: (data) => {
      getUserMutation.mutate();
      navigate('/route');
    },
    onError: (error: any) => {
      if (error.response.data.errors) {
        const loginError: LoginError = error.response.data.errors;
        setError(loginError);
      } else {
        setError({ Error: ['Ошибка авторизации'] });
      }
    },
  });

  const getUserMutation = useMutation(UserService.me, {
    onSuccess: (user) => setUser(user),
  });

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    mutation.mutate({ email, password });
  };

  if (UserService.isAuthenticated()) {
    return <Navigate to={'/route'} />;
  }

  return (
    <Box
      component="form"
      onSubmit={handleSubmit}
      sx={{ width: 300, margin: 'auto' }}
    >
      <Typography variant="h6" gutterBottom>
        Вход
      </Typography>
      <TextField
        label="Email"
        type="email"
        fullWidth
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        required
        margin="normal"
      />
      <TextField
        label="Пароль"
        type="password"
        fullWidth
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        required
        margin="normal"
      />
      <Button
        type="submit"
        fullWidth
        variant="contained"
        color="primary"
        sx={{ marginTop: 2 }}
      >
        Войти
      </Button>
      {error &&
        Object.values(error).map((value) =>
          value.map((errorStr) => (
            <Typography
              key={errorStr}
              color="error"
              variant="body2"
              sx={{ marginTop: 2 }}
            >
              {errorStr}
            </Typography>
          )),
        )}
    </Box>
  );
};

export default LoginPage;
