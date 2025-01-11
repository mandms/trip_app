import { ICreateImage, IImage } from './types.image';
import { ICoordinates } from './types.coordinates';

export interface ICreateLocation {
  name: string;
  description: string;
  coordinates: ICoordinates;
  images: ICreateImage[];
}

export interface IUpdateLocation {
  name: string;
  description: string;
  coordinates: ICoordinates;
  order: number;
}

export interface ILocation {
  id: string;
  name: string;
  description: string;
  coordinates: ICoordinates;
  order: number;
  images: IImage[];
}
