import { CardMedia } from '@mui/material';
import defaultImage from '../../images/location-default.jpg';

export default function ImageCard({ imagePath }: { imagePath: string }) {
  return (
    <CardMedia
      component="img"
      height="100%"
      sx={{ objectFit: 'contain' }}
      image={`${process.env.REACT_APP_API_RESOURCES}/${imagePath}`}
      alt="Image preview"
      onError={(e: React.SyntheticEvent<HTMLImageElement, Event>) => {
        e.currentTarget.src = defaultImage;
      }}
    />
  );
}
