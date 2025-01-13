import { createContext } from 'react';
import { IUser } from '../types/types.user';

export const CurrentUserContext = createContext<{
  user: IUser | null;
  setUser: (user: IUser | null) => void;
}>({
  user: null,
  setUser: () => ({}),
});
