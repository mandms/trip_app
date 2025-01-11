import React from 'react';
import { Route, Routes } from 'react-router-dom';
import RequireAuth from './Auth/RequireAuth';
import RouteTable from './Route/all/RouteTable';
import Login from './Auth/Login';
import TagTable from './Tag/TagTable';
import UserTable from './User/UserTable';
import MomentTable from './Moment/MomentTable';
import ReviewTable from './Review/ReviewTable';
import { Box } from '@mui/material';
import PageNotFound from './Exceptions/PageNotFound';
const Content: React.FC = () => {
  return (
    <Box flex={1} p={2}>
      <Routes>
        <Route
          path="/route"
          element={
            <RequireAuth>
              <RouteTable />
            </RequireAuth>
          }
        />
        <Route
          path="/tag"
          element={
            <RequireAuth>
              <TagTable />
            </RequireAuth>
          }
        />
        <Route
          path="/user"
          element={
            <RequireAuth>
              <UserTable />
            </RequireAuth>
          }
        />
        <Route
          path="/moment"
          element={
            <RequireAuth>
              <MomentTable />
            </RequireAuth>
          }
        />
        <Route
          path="/review"
          element={
            <RequireAuth>
              <ReviewTable />
            </RequireAuth>
          }
        />
        <Route path="*" element={<PageNotFound />} />
        <Route path="/" element={<Login />} />
      </Routes>
    </Box>
  );
};

export default Content;
