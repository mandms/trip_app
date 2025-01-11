import { IAuthor } from './types.user';

export interface ICreateReview {
  text: string;
  rate: number;
}

export interface IReview {
  id: string;
  text: string;
  createdAt: string;
  user: IAuthor;
  rate: number;
}

export interface IUpdateReview {
  text: string;
  id: string;
  rate: number;
}

export interface IReviews {
  data: IReview[];
  totalRecords: number;
}
