import axios from 'axios';

export const instance = axios.create({
  baseURL: 'http://localhost:5218/api/',
});

instance.interceptors.request.use((config) => {
  config.headers.Authorization = `Bearer ${localStorage.getItem('token')}`;
  return config;
});

instance.interceptors.response.use(
  (config) => config,
  async (error) => {
    if (error.response) {
      const originalRequest = { ...error.config };
      if (
        error.response.status === 401 &&
        originalRequest &&
        !originalRequest._isRetry
      ) {
        localStorage.removeItem('token');
        window.location.href = '/';
      }
    } else {
      console.log('ERROR');
    }

    return Promise.reject(error);
  },
);
