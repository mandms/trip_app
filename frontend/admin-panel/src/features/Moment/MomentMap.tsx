import React, { useEffect, useState } from 'react';
import {
  MapContainer,
  TileLayer,
  Marker,
  Popup,
  useMapEvents,
  Tooltip,
  useMap,
} from 'react-leaflet';
import { LatLngExpression, LatLngLiteral } from 'leaflet';
import 'leaflet/dist/leaflet.css';
import '../../components/Map/Map.css';
import logo from '../../images/location-map-default.png';
import { IImage } from '../../types/types.image';
import { MapLocationIcon } from '../../components/Map/MapIcon';

const MapClickHandler: React.FC<{
  setPoint: React.Dispatch<React.SetStateAction<LatLngLiteral | null>>;
  addLocation: (latitude: number, longitude: number) => void;
}> = ({ setPoint, addLocation }) => {
  useMapEvents({
    click(e) {
      const { lat, lng } = e.latlng;
      setPoint(e.latlng);
      addLocation(lat, lng);
    },
  });
  return null;
};

interface ICreateRouteMapProps {
  addLocation?: (latitude: number, longitude: number) => void;
  location?: { coords?: LatLngLiteral; image?: IImage };
}

const MomentMap: React.FC<ICreateRouteMapProps> = ({
  addLocation,
  location,
}) => {
  const [point, setPoint] = useState<LatLngLiteral | null>(null);

  useEffect(() => {
    if (!location || !location.coords) return;
    setPoint(location.coords);
  }, [location]);

  const mapZoom = 13;
  const defaultCenter = { lat: 56.63616, lng: 47.9035 };

  const UpdateMapCenter = () => {
    const map = useMap();
    let center = defaultCenter;
    if (point) center = point;
    map.flyTo(center, mapZoom);
    return null;
  };

  return (
    <div style={location ? { height: '100%' } : { height: '350px' }}>
      <MapContainer
        zoom={13}
        center={point ?? defaultCenter}
        style={{ height: '100%' }}
      >
        <UpdateMapCenter />
        <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
        {addLocation && (
          <MapClickHandler setPoint={setPoint} addLocation={addLocation} />
        )}
        {point && (
          <Marker
            icon={MapLocationIcon(location?.image?.path)}
            position={point}
          >
            <Popup>
              <div>
                <h3>Point</h3>
                <p>
                  Coordinates: {(point as LatLngLiteral).lat},{' '}
                  {(point as LatLngLiteral).lng}
                </p>
                <img
                  src={
                    location
                      ? `${process.env.REACT_APP_API_RESOURCES}/${location.image?.path}`
                      : logo
                  }
                  alt="Point logo"
                  style={{ width: '100px', height: '100px' }}
                />
              </div>
            </Popup>
            <Tooltip direction="right" offset={[0, 0]} opacity={1} permanent>
              Point
            </Tooltip>
          </Marker>
        )}
      </MapContainer>
    </div>
  );
};

export default MomentMap;
