import React, { useEffect, useState } from 'react';
import {
  TextField,
  Button,
  DialogContent,
  Dialog,
  DialogTitle,
  DialogActions,
  Box,
  Avatar,
  Alert,
} from '@mui/material';
import { useMutation, useQueryClient } from 'react-query';
import UserService from '../../api/api.user';
import { IUpdateUser } from '../../types/types.user';
import { toBase64 } from '../../utils/fileUtils';
import { parseErrorMessage } from '../../utils/errorMessageParser';

interface IUpdateUserProps {
  close: () => void;
  open: boolean;
  updatedUser: IUpdateUser;
  userId: string;
}

const UpdateUserModal: React.FC<IUpdateUserProps> = ({
  close,
  open,
  updatedUser,
  userId,
}) => {
  const [user, setUser] = useState<IUpdateUser>({
    username: '',
    biography: '',
  });
  const [userImage, setUserImage] = useState<string | undefined>();
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    setUser({
      username: updatedUser.username,
      biography: updatedUser.biography,
    });
    setUserImage(updatedUser?.avatar?.image);
  }, [updatedUser]);

  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: async () => {
      if (user.avatar) {
        user.avatar.image = user.avatar.image.replace(
          /^data:image\/\w+;base64,/,
          '',
        );
      }
      await UserService.update(userId, user);
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries('users');
      close();
    },
    onError: (error: unknown) => {
      const errorMessage = parseErrorMessage(error);
      setError(errorMessage);
    },
  });

  const handleChange = (field: string, value: string) => {
    setUser((prev) => ({
      ...prev,
      [field]: value,
    }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    mutation.mutate();
  };

  const handleImageUpload = async (e: React.ChangeEvent<HTMLInputElement>) => {
    const files = e.target.files;

    if (!files) {
      return;
    }

    const base64Image = await toBase64(files[0]);

    setUser((prev) => ({
      ...prev,
      avatar: { image: base64Image },
    }));
  };

  const handleSaveChanges = () => {
    console.log(user);
    //mutation.mutate();
  };

  const avatar =
    user.avatar !== undefined
      ? user?.avatar?.image
      : `${process.env.REACT_APP_API_RESOURCES}/${userImage}`;

  return (
    <Dialog
      open={open}
      onClose={close}
      maxWidth="sm"
      fullWidth
      aria-modal={true}
    >
      <DialogTitle>Изменить данные пользователя</DialogTitle>
      <DialogContent>
        <form onSubmit={handleSubmit}>
          <Box sx={{ p: 1 }}>
            <TextField
              label="Имя пользователя"
              variant="outlined"
              fullWidth
              value={user.username}
              onChange={(e) => handleChange('username', e.target.value)}
              required
              sx={{ mb: 2 }}
            />
            <TextField
              label="Биография"
              variant="outlined"
              fullWidth
              value={user.biography}
              onChange={(e) => handleChange('biography', e.target.value)}
              multiline
              rows={4}
              sx={{ mb: 2 }}
            />
            <Button variant="contained" component="label">
              Загрузить аватарку
              <input
                type="file"
                accept="image/*"
                hidden
                onChange={(e) => handleImageUpload(e)}
              />
              <Avatar sx={{ marginLeft: 2 }} src={avatar} />
            </Button>
            {error && (
              <Alert severity="error" sx={{ marginTop: 2 }}>
                {error}
              </Alert>
            )}
            <Button
              type="submit"
              variant="contained"
              color="primary"
              fullWidth
              sx={{ marginTop: 2 }}
              onClick={handleSaveChanges}
            >
              Сохранить изменения
            </Button>
          </Box>
        </form>
      </DialogContent>
      <DialogActions>
        <Button onClick={close} color="secondary">
          Закрыть
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default UpdateUserModal;
export type { IUpdateUserProps };
