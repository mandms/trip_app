import React, { useEffect, useState } from 'react';
import { Box, Button, Card, IconButton, Typography } from '@mui/material';
import { useMutation } from 'react-query';
import DeleteIcon from '@mui/icons-material/Delete';
import AddPhotoAlternateIcon from '@mui/icons-material/AddPhotoAlternate';
import { ICreateImage, IImage } from '../../types/types.image';
import { removeBase64Prefix } from '../../utils/removeBase64Prefix';
import ImageCard from './ImageCard';

interface IImageManagerProps {
  images: IImage[];
  id: string;
  addImages: (id: string, images: ICreateImage[]) => Promise<any>;
  removeImages: (id: string, images: string[]) => Promise<any>;
  invalidateQueries: () => Promise<any>;
}

const ImageManager: React.FC<IImageManagerProps> = ({
  images,
  id,
  addImages,
  removeImages,
  invalidateQueries,
}) => {
  const [localImages, setLocalImages] = useState<IImage[]>(images);
  const [selectedImages, setSelectedImages] = useState<Set<string>>(new Set());

  useEffect(() => {
    setLocalImages(images);
  }, [images]);

  const addImageMutation = useMutation(
    async (newImages: ICreateImage[]) =>
      await addImages(
        id,
        newImages.map((image) => {
          return {
            image: removeBase64Prefix(image.image),
          };
        }),
      ),
    {
      onSuccess: async () => {
        await invalidateQueries();
      },
    },
  );

  const deleteImageMutation = useMutation(
    async (deletedImages: string[]) => await removeImages(id, deletedImages),
    {
      onSuccess: async () => {
        await invalidateQueries();
      },
    },
  );

  const handleAddImages = (event: React.ChangeEvent<HTMLInputElement>) => {
    const files = event.target.files;
    if (files) {
      const fileReaders = Array.from(files).map((file) => {
        return new Promise<{ image: string }>((resolve, reject) => {
          const reader = new FileReader();
          reader.onload = () => resolve({ image: reader.result as string });
          reader.onerror = reject;
          reader.readAsDataURL(file);
        });
      });

      Promise.all(fileReaders)
        .then((base64Images) => {
          addImageMutation.mutate(base64Images);
        })
        .catch((err) => console.error('Error reading files', err));
    }
  };

  const handleSelectImage = (imageId: string) => {
    setSelectedImages((prev) => {
      const newSet = new Set(prev);
      if (newSet.has(imageId)) {
        newSet.delete(imageId);
      } else {
        newSet.add(imageId);
      }
      return newSet;
    });
  };

  const handleDeleteSelectedImages = () => {
    const imagesToDelete = localImages.filter((img) =>
      selectedImages.has(img.id),
    );
    const deletedImages = imagesToDelete.map((img) => img.id);
    deleteImageMutation.mutate(deletedImages);
    setSelectedImages(new Set());
  };

  return (
    <Box>
      <Typography variant="h6">Изображения</Typography>
      <Box
        sx={{
          display: 'grid',
          gridTemplateColumns: 'repeat(auto-fill, minmax(150px, 1fr))',
          gap: 2,
        }}
      >
        {localImages.map((image) => (
          <Card
            key={image.id}
            sx={{
              position: 'relative',
              boxSizing: 'border-box',
              outline: selectedImages.has(image.id) ? '3px solid red' : 'none',
            }}
          >
            <ImageCard imagePath={image.path} />
            <IconButton
              sx={{
                position: 'absolute',
                top: 5,
                right: 5,
                backgroundColor: 'rgba(0, 0, 0, 0.5)',
                color: 'white',
              }}
              onClick={(e) => {
                e.stopPropagation();
                handleSelectImage(image.id);
              }}
            >
              <DeleteIcon />
            </IconButton>
          </Card>
        ))}
      </Box>
      <Button
        variant="contained"
        component="label"
        startIcon={<AddPhotoAlternateIcon />}
        sx={{ mt: 2, mr: 2 }}
      >
        Add Images
        <input
          type="file"
          accept="image/*"
          multiple
          hidden
          onChange={handleAddImages}
        />
      </Button>
      <Button
        variant="contained"
        color="error"
        startIcon={<DeleteIcon />}
        sx={{ mt: 2 }}
        onClick={handleDeleteSelectedImages}
        disabled={selectedImages.size === 0}
      >
        Delete Selected Images
      </Button>
    </Box>
  );
};

export default ImageManager;
