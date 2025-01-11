export interface IFilter {
  tagFilter?: string;
  dateFilter?: DateRange;
  sort?: string;
  order?: OrderType;
  search?: string;
}

export type DateRange = { start?: string; end?: string };

export type FilterParamsType = 'date' | 'tag';

export type OrderType = 'asc' | 'desc';
