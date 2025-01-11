import { ICoordinates } from './types.coordinates';
import { ICreateImage, IImage } from './types.image';
import { IAuthor } from './types.user';

export interface ICreateMoment {
  description: string;
  coordinates: ICoordinates;
  status: number;
  images: ICreateImage[];
}

export interface IUpdateMoment {
  description: string;
  coordinates: ICoordinates;
  status: number;
}

export interface IMoment {
  id: string;
  description: string;
  coordinates: ICoordinates;
  createdAt: string;
  user: IAuthor;
  status: number;
  images: IImage[];
}

export interface IMoments {
  data: IMoment[];
  totalRecords: number;
}
