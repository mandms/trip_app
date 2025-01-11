import { Navigate } from 'react-router-dom';
import React from 'react';
import UserService from '../../api/api.user';

interface RequireAuthProps {
  children: React.ReactNode;
}

const RequireAuth: React.FC<RequireAuthProps> = ({ children }) => {
  if (UserService.isAuthenticated()) {
    return <>{children}</>;
  }
  return <Navigate replace to="/" />;
};

export default RequireAuth;
