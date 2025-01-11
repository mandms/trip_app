import defaultLocationImage from '../../images/location-map-default.png';
import L from 'leaflet';

export const MapLocationIcon = (image?: string) => {
  const apiUrl = process.env.REACT_APP_API_RESOURCES;
  return new L.Icon({
    iconUrl: image ? `${apiUrl}/${image}` : defaultLocationImage,
    iconRetinaUrl: image ? `${apiUrl}/${image}` : defaultLocationImage,
    iconSize: new L.Point(40, 40),
    className: 'map-location-icon',
  });
};
