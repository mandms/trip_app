import { instance } from './api.config';
import { ICreateReview } from '../types/types.review';
import { ApiBase } from './api.base';
import { IFilter } from '../types/types.filter';

const ReviewService = {
  async getAll(page: number, limit: number, params?: IFilter) {
    return await ApiBase.getAll('/review', page, limit, params);
  },

  async getById(id: string) {
    const { data } = await instance.get(`/review/${id}`);
    return data;
  },

  async create(routeId: string, review: ICreateReview) {
    const { data } = await instance.post(`/review/route/${routeId}`, review);
    return data;
  },

  async update(id: string, review: { text: string; rate: number }) {
    const { data } = await instance.put(`/review/${id}`, review);
    return data;
  },

  async delete(id: string) {
    const { data } = await instance.delete(`/review/${id}`);
    return data;
  },
};

export default ReviewService;
