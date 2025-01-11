export interface ICategory {
  id: string;
  name: string;
}

export interface ITag {
  id: string;
  name: string;
  category: ICategory;
}

export interface IRouteTag {
  id: string;
  name: string;
}

export interface ITags {
  data: ITag[];
  totalRecords: number;
}
