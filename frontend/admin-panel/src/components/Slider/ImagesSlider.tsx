import { Box, IconButton, Typography } from '@mui/material';
import ArrowBackIosIcon from '@mui/icons-material/ArrowBackIos';
import ArrowForwardIosIcon from '@mui/icons-material/ArrowForwardIos';
import React, { useState } from 'react';
import { IImage } from '../../types/types.image';
import ImageCard from '../Image/ImageCard';

const ImagesSlider: React.FC<{ images?: IImage[] }> = ({ images }) => {
  const [currentSlide, setCurrentSlide] = useState(0);

  const handlePrevSlide = () => {
    setCurrentSlide((prev) => (prev > 0 ? prev - 1 : 0));
  };

  const handleNextSlide = (
    e: React.MouseEvent<HTMLButtonElement, MouseEvent>,
    imagesLength: number,
  ) => {
    setCurrentSlide((prev) => (prev < imagesLength - 1 ? prev + 1 : prev));
  };

  if (!images || !images?.length) return <Typography>Картинок нет</Typography>;

  return (
    <Box position="relative" height={'100%'}>
      <Box
        display="flex"
        justifyContent="center"
        alignItems="center"
        overflow="hidden"
        height={'100%'}
      >
        <ImageCard imagePath={images[currentSlide].path} />
      </Box>
      <IconButton
        onClick={handlePrevSlide}
        disabled={currentSlide === 0}
        style={{
          position: 'absolute',
          left: 0,
          top: '50%',
          transform: 'translateY(-50%)',
          background: '#00000074',
          color: '#fff',
        }}
      >
        <ArrowBackIosIcon />
      </IconButton>
      <IconButton
        onClick={(e) => handleNextSlide(e, images.length)}
        disabled={currentSlide === images.length - 1}
        style={{
          position: 'absolute',
          right: 0,
          top: '50%',
          transform: 'translateY(-50%)',
          background: '#00000074',
          color: '#fff',
        }}
      >
        <ArrowForwardIosIcon />
      </IconButton>
    </Box>
  );
};

export default ImagesSlider;
