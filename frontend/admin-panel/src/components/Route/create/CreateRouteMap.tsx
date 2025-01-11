import React, { useState } from 'react';
import {
  MapContainer,
  TileLayer,
  Marker,
  Polyline,
  Popup,
  useMapEvents,
  Tooltip,
} from 'react-leaflet';
import { LatLngExpression } from 'leaflet';
import 'leaflet/dist/leaflet.css';
import '../detail/RouteMap.css';
import logo from '../../../logo.svg';
import L from 'leaflet';
import { useRouteMap } from '../../../hooks/useRouteMap';

const getLeafletImage = () => {
  return new L.Icon({
    iconUrl: logo,
    iconRetinaUrl: logo,
    iconSize: new L.Point(40, 40),
    className: 'location-card-image',
  });
};

const MapClickHandler: React.FC<{
  setPoints: React.Dispatch<React.SetStateAction<LatLngExpression[]>>;
  addLocation: (latitude: number, longitude: number) => void;
  pointsCount: number;
}> = ({ setPoints, addLocation, pointsCount }) => {
  useMapEvents({
    click(e) {
      if (pointsCount > 4) return;
      const { lat, lng } = e.latlng;
      setPoints((prevPoints) => [...prevPoints, [lat, lng]]);
      addLocation(lat, lng);
    },
  });
  return null;
};

interface ICreateRouteMapProps {
  addLocation: (latitude: number, longitude: number) => void;
}

const CreateRouteMap: React.FC<ICreateRouteMapProps> = ({ addLocation }) => {
  const [points, setPoints] = useState<LatLngExpression[]>([]);

  const result = useRouteMap(points);

  return (
    <div style={{ height: '350px' }}>
      <MapContainer
        center={[56.63716897552745, 47.894257726140566]}
        zoom={13}
        style={{ height: '100%' }}
      >
        <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
        <MapClickHandler
          setPoints={setPoints}
          addLocation={addLocation}
          pointsCount={points.length}
        />
        {points.map((point, index) => (
          <Marker icon={getLeafletImage()} key={index} position={point}>
            <Popup>
              <div>
                <h3>Point {index + 1}</h3>
                <p>
                  Coordinates: {(point as [number, number])[0]},{' '}
                  {(point as [number, number])[1]}
                </p>
                <img
                  src={logo}
                  alt="Point logo"
                  style={{ width: '100px', height: '100px' }}
                />
              </div>
            </Popup>
            <Tooltip direction="right" offset={[0, 0]} opacity={1} permanent>
              Point {index}
            </Tooltip>
          </Marker>
        ))}
        {result.route.length > 1 && (
          <Polyline positions={result.route} color="blue" />
        )}
        {result.loading && <p>Loading route...</p>}
      </MapContainer>
    </div>
  );
};

export default CreateRouteMap;
