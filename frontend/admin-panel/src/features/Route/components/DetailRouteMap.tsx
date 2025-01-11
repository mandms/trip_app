import React, { useState, useEffect } from 'react';
import {
  MapContainer,
  TileLayer,
  Marker,
  Polyline,
  Popup,
  Tooltip,
  useMap,
} from 'react-leaflet';
import { LatLngExpression, LatLngLiteral } from 'leaflet';
import 'leaflet/dist/leaflet.css';
import '../../../components/Map/Map.css';
import { ILocation } from '../../../types/types.location';
import { useRouteMap } from '../../../hooks/useRouteMap';
import { MapLocationIcon } from '../../../components/Map/MapIcon';

export interface IRouteMapProps {
  locations: ILocation[];
  scopeMapCoords: LatLngLiteral;
  zoom?: number;
}

const DetailRouteMap: React.FC<IRouteMapProps> = ({
  locations,
  scopeMapCoords,
}) => {
  const [points, setPoints] = useState<LatLngExpression[]>([]);

  useEffect(() => {
    setPoints(
      locations.map((location) => [
        location.coordinates.latitude,
        location.coordinates.longitude,
      ]),
    );
  }, [locations]);

  const mapZoom = 13;

  const UpdateMapCenter = (props: { center: LatLngLiteral }) => {
    const map = useMap();
    map.flyTo(props.center, mapZoom);
    return null;
  };

  const result = useRouteMap(points);

  return (
    <div style={{ height: '500px' }}>
      <MapContainer
        center={scopeMapCoords}
        zoom={mapZoom}
        style={{ height: '250px' }}
      >
        <UpdateMapCenter center={scopeMapCoords} />
        <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
        {locations.map((location, index) => (
          <Marker
            icon={MapLocationIcon(
              location.images.length ? location.images[0].path : '',
            )}
            key={index}
            position={[
              location.coordinates.latitude,
              location.coordinates.longitude,
            ]}
          >
            <Popup>
              <div>
                <h3>{location.name}</h3>
                <p>{location.description}</p>
                <img
                  src={`${process.env.REACT_APP_API_RESOURCES}/${location.images[0]}`}
                  alt="Point logo"
                  style={{ width: '100px', height: '100px' }}
                />
              </div>
            </Popup>
            <Tooltip direction="right" offset={[0, 0]} opacity={1} permanent>
              {index === 0 && 'Начало '}
              {index === locations.length - 1 && 'Конец '}
              {location.name}
            </Tooltip>
          </Marker>
        ))}
        {result.route.length > 1 && (
          <Polyline positions={result.route} color="blue" />
        )}
      </MapContainer>
      {result.loading && <p>Loading route...</p>}
    </div>
  );
};

export default DetailRouteMap;
