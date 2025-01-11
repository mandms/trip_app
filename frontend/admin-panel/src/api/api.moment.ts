import { instance } from './api.config';
import { ICreateMoment, IMoment } from '../types/types.moment';
import { ICreateImage } from '../types/types.image';
import { ApiBase } from './api.base';
import { IFilter } from '../types/types.filter';

const MomentService = {
  async getAll(page: number, limit: number, params?: IFilter) {
    return await ApiBase.getAll('/moment', page, limit, params);
  },

  async getById(id: string): Promise<IMoment> {
    const { data } = await instance.get(`/moment/${id}`);
    return data;
  },

  async create(moment: ICreateMoment) {
    const { data } = await instance.post('/moment', moment);
    return data;
  },

  async update(
    id: string,
    moment: {
      description: string;
      coordinates: { latitude: number; longitude: number };
      status: number;
    },
  ) {
    const { data } = await instance.put(`/moment/${id}`, moment);
    return data;
  },

  async delete(id: string) {
    const { data } = await instance.delete(`/moment/${id}`);
    return data;
  },

  async removeImages(id: string, images: string[]) {
    const { data } = await instance.delete(`/moment/${id}/images`, {
      data: images,
    });
    return data;
  },

  async addImages(id: string, images: ICreateImage[]) {
    const { data } = await instance.post(`/moment/${id}/images`, { images });
    return data;
  },
};

export default MomentService;
