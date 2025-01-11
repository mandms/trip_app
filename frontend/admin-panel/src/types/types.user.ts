import { ICreateImage } from './types.image';

export interface IUser {
  id: string;
  username: string;
  email: string;
  avatar: string;
  biography: string;
  roles: string[];
}

export interface IUsers {
  data: IUser[];
  totalRecords: number;
}

export interface IUpdateUser {
  username: string;
  biography: string;
  avatar?: ICreateImage;
}

export interface ICreateUser {
  username: string;
  email: string;
  password: string;
}

export interface IAuthor {
  id: string;
  username: string;
  avatar: string;
}
