import { instance } from './api.config';
import {
  ICreateRoute,
  IDetailRoute,
  IRoutes,
  IUpdateRoute,
} from '../types/types.route';
import { IFilter } from '../types/types.filter';
import { ApiBase } from './api.base';

const RouteService = {
  async getAll(
    page: number,
    limit: number,
    params?: IFilter,
  ): Promise<IRoutes> {
    return await ApiBase.getAll('/route', page, limit, params);
  },

  async getById(id: string): Promise<IDetailRoute> {
    const { data } = await instance.get(`/route/${id}`);
    return data;
  },

  async create(route: ICreateRoute) {
    const { data } = await instance.post('/route', route);
    return data;
  },

  async update(id: string, route: IUpdateRoute) {
    const { data } = await instance.put(`/route/${id}`, route);
    return data;
  },

  async addTags(id: string, tags: string[]) {
    const { data } = await instance.post(`/route/${id}/tags`, tags);
    return data;
  },

  async removeTags(id: string, tags: string[]) {
    const { data } = await instance.delete(`/route/${id}/tags`, { data: tags });
    return data;
  },

  async delete(id: string) {
    const { data } = await instance.delete(`/route/${id}`);
    return data;
  },
};

export default RouteService;
