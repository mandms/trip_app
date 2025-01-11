import { instance } from './api.config';

const CategoryService = {
  async getAll(page: number, limit: number) {
    const { data } = await instance.get('/category', {
      params: { PageNumber: page, PageSize: limit },
    });
    return data;
  },
};

export default CategoryService;
