import { instance } from './api.config';
import { ICreateUser, IUpdateUser } from '../types/types.user';
import { ApiBase } from './api.base';
import { IFilter } from '../types/types.filter';

const UserService = {
  async login(credentials: { email: string; password: string }) {
    const { data } = await instance.post('/user/login', credentials);
    localStorage.setItem('token', data);
    return data;
  },

  isAuthenticated() {
    return !!localStorage.getItem('token');
  },

  logout() {
    if (localStorage.getItem('token')) {
      localStorage.removeItem('token');
    }
  },

  async getAll(page: number, limit: number, params?: IFilter) {
    return await ApiBase.getAll('/user', page, limit, params);
  },

  async me() {
    const { data } = await instance.get(`/user/me`);
    return data;
  },

  async create(user: ICreateUser) {
    const { data } = await instance.post('/user/registration', user);
    return data;
  },

  async update(id: string, user: IUpdateUser) {
    const { data } = await instance.put(`/user/${id}`, user);
    return data;
  },

  async delete(id: string) {
    const { data } = await instance.delete(`/user/${id}`);
    return data;
  },
};

export default UserService;
