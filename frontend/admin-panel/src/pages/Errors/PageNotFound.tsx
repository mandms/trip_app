import React from 'react';
import { Box, Button, Typography } from '@mui/material';
import { useNavigate } from 'react-router-dom';

const PageNotFound = () => {
  const navigate = useNavigate();

  const handleGoHome = () => {
    navigate('/');
  };

  return (
    <Box
      display="flex"
      flexDirection="column"
      justifyContent="center"
      alignItems="center"
      height="100vh"
      textAlign="center"
      p={2}
    >
      <Typography variant="h1" color="error" fontWeight="bold">
        404
      </Typography>
      <Typography variant="h5" gutterBottom>
        Уупс! Страница, которую вы пытаетесь найти, несуществует.
      </Typography>
      <Typography variant="body1" color="textSecondary" gutterBottom>
        Возможно она была удалена, переименована или никогда не существовала.
      </Typography>
      <Button
        variant="contained"
        color="primary"
        onClick={handleGoHome}
        sx={{ mt: 3 }}
      >
        Куда-нибудь
      </Button>
    </Box>
  );
};

export default PageNotFound;
