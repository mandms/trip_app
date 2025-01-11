import { instance } from './api.config';
import { ICreateLocation, IUpdateLocation } from '../types/types.location';
import { ICreateImage } from '../types/types.image';

const LocationService = {
  async getAll(page: number, limit: number) {
    const { data } = await instance.get('/location', {
      params: { PageNumber: page, PageSize: limit },
    });
    return data;
  },

  async getById(id: string) {
    const { data } = await instance.get(`/location/${id}`);
    return data;
  },

  async create(routeId: string, location: ICreateLocation) {
    const { data } = await instance.post(
      `/location/route/${routeId}`,
      location,
    );
    return data;
  },

  async update(id: string, location: IUpdateLocation) {
    const { data } = await instance.put(`/location/${id}`, location);
    return data;
  },

  async delete(id: string) {
    const { data } = await instance.delete(`/location/${id}`);
    return data;
  },

  async removeImages(id: string, images: string[]) {
    const { data } = await instance.delete(`/location/${id}/images`, {
      data: images,
    });
    return data;
  },

  async addImages(id: string, images: ICreateImage[]) {
    const { data } = await instance.post(`/location/${id}/images`, images);
    return data;
  },
};

export default LocationService;
