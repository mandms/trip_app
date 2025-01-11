import React, { useEffect, useState } from 'react';
import {
  MapContainer,
  TileLayer,
  Marker,
  Popup,
  useMapEvents,
  Tooltip,
} from 'react-leaflet';
import { LatLngExpression, LatLngLiteral } from 'leaflet';
import 'leaflet/dist/leaflet.css';
import '../../components/Map/Map.css';
import logo from '../../images/location-map-default.png';
import { IImage } from '../../types/types.image';
import { MapLocationIcon } from '../../components/Map/MapIcon';

const MapClickHandler: React.FC<{
  setPoint: React.Dispatch<React.SetStateAction<LatLngExpression | null>>;
  addLocation: (latitude: number, longitude: number) => void;
}> = ({ setPoint, addLocation }) => {
  useMapEvents({
    click(e) {
      const { lat, lng } = e.latlng;
      setPoint([lat, lng]);
      addLocation(lat, lng);
    },
  });
  return null;
};

interface ICreateRouteMapProps {
  addLocation?: (latitude: number, longitude: number) => void;
  location?: { coords?: LatLngExpression; image?: IImage };
}

const MomentMap: React.FC<ICreateRouteMapProps> = ({
  addLocation,
  location,
}) => {
  const [point, setPoint] = useState<LatLngExpression | null>(null);

  useEffect(() => {
    if (!location || !location.coords) return;
    setPoint(location.coords);
  }, [location]);

  return (
    <div style={location ? { height: '100%' } : { height: '350px' }}>
      <MapContainer
        center={location?.coords ?? [43, 56]}
        zoom={13}
        style={{ height: '100%' }}
      >
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
