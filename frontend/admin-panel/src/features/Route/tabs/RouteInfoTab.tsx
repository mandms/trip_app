import { Box, FormControlLabel, Grid2, Switch, TextField } from '@mui/material';
import React from 'react';

interface IRouteInfo {
  name: string;
  description: string;
  duration: number;
  status: number;
}

interface IRouteInfoProps {
  route: IRouteInfo;
  handleChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  handleStatusToggle: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

const RouteInfoTab: React.FC<IRouteInfoProps> = ({
  route,
  handleChange,
  handleStatusToggle,
}) => {
  return (
    <>
      <Grid2 size={{ xs: 12 }}>
        <TextField
          label="Название маршрута"
          variant="outlined"
          fullWidth
          name="name"
          value={route.name}
          onChange={handleChange}
          required
        />
      </Grid2>
      <Grid2 size={{ xs: 12 }}>
        <TextField
          label="Описание маршрута"
          variant="outlined"
          fullWidth
          name="description"
          value={route.description}
          onChange={handleChange}
          required
          multiline
          rows={4}
        />
      </Grid2>
      <Grid2 size={{ xs: 12, sm: 6 }}>
        <TextField
          label="Длительность (в часах)"
          variant="outlined"
          fullWidth
          name="duration"
          value={route.duration}
          onChange={handleChange}
          required
          type="number"
        />
      </Grid2>
      <FormControlLabel
        control={
          <Switch
            checked={route.status === 0}
            onChange={(e) => handleStatusToggle(e)}
            name="status"
            color="primary"
          />
        }
        label="Скрыть от других пользователей"
      />
    </>
  );
};

export default RouteInfoTab;
