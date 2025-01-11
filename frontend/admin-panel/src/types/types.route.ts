import { ICreateLocation, ILocation } from './types.location';
import { IAuthor } from './types.user';
import { IRouteTag, ITag } from './types.tag';

export interface IDetailRoute {
  id: string;
  name: string;
  description: string;
  duration: number;
  user: IAuthor;
  tags: IRouteTag[];
  locations: ILocation[];
  status: number;
  state: number;
}

export interface ICreateRoute {
  name: string;
  description: string;
  duration: number;
  tags: number[];
  locations: ICreateLocation[];
  status: number;
}

export interface IRoute {
  id: string;
  name: string;
  description: string;
  duration: number;
  user: IAuthor;
  status: number;
  rating: number;
  tags: ITag[];
}

export interface IRoutes {
  data: IRoute[];
  totalRecords: number;
}

export interface IUpdateRoute {
  id: string;
  name: string;
  description: string;
  duration: number;
  status: number;
}
