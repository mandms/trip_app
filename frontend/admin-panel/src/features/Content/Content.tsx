import React from 'react';
import { Route, Routes } from 'react-router-dom';
import RequireAuth from '../Auth/RequireAuth';
import RoutePage from '../../pages/Route/RoutePage';
import LoginPage from '../../pages/Auth/LoginPage';
import TagPage from '../../pages/Tag/TagPage';
import UserPage from '../../pages/User/UserPage';
import MomentPage from '../../pages/Moment/MomentPage';
import ReviewPage from '../../pages/Review/ReviewPage';
import { Box } from '@mui/material';
import PageNotFound from '../../pages/Errors/PageNotFound';
const Content: React.FC = () => {
  return (
    <Box flex={1} p={2}>
      <Routes>
        <Route
          path="/route"
          element={
            <RequireAuth>
              <RoutePage />
            </RequireAuth>
          }
        />
        <Route
          path="/tag"
          element={
            <RequireAuth>
              <TagPage />
            </RequireAuth>
          }
        />
        <Route
          path="/user"
          element={
            <RequireAuth>
              <UserPage />
            </RequireAuth>
          }
        />
        <Route
          path="/moment"
          element={
            <RequireAuth>
              <MomentPage />
            </RequireAuth>
          }
        />
        <Route
          path="/review"
          element={
            <RequireAuth>
              <ReviewPage />
            </RequireAuth>
          }
        />
        <Route path="*" element={<PageNotFound />} />
        <Route path="/" element={<LoginPage />} />
      </Routes>
    </Box>
  );
};

export default Content;
