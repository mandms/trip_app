import { IFilter } from '../types/types.filter';
import { instance } from './api.config';

export const ApiBase = {
  async getAll(
    url: string,
    page: number,
    limit: number,
    params?: IFilter,
  ): Promise<any> {
    const { data } = await instance.get(url, {
      params: {
        PageNumber: page,
        PageSize: limit,
        tag: params?.tagFilter,
        startDate: params?.dateFilter?.start,
        endDate: params?.dateFilter?.end,
        sortBy: params?.sort,
        ordering: params?.order,
        searchTerm: params?.search,
      },
    });
    return data;
  },
};
