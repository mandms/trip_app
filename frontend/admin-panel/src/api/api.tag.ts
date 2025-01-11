import { instance } from './api.config';
import { ITags } from '../types/types.tag';
import { ApiBase } from './api.base';
import { IFilter } from '../types/types.filter';

const TagService = {
  async getAll(page = 1, limit = 100, params?: IFilter): Promise<ITags> {
    return await ApiBase.getAll('/tag', page, limit, params);
  },

  async getById(id: string) {
    const { data } = await instance.get(`/tag/${id}`);
    return data;
  },

  async create(categoryId: string, tag: string) {
    const { data } = await instance.post(`/tag/category/${categoryId}`, {
      name: tag,
    });
    return data;
  },

  async update(id: string, tag: string, categoryId: string) {
    const { data } = await instance.put(`/tag/${id}`, {
      name: tag,
      categoryId: categoryId,
    });
    return data;
  },

  async delete(id: string) {
    const { data } = await instance.delete(`/tag/${id}`);
    return data;
  },
};

export default TagService;
