import { LatLngExpression } from 'leaflet';
import { useEffect, useState } from 'react';

export const useRouteMap = (points: LatLngExpression[]) => {
  const [route, setRoute] = useState<LatLngExpression[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const apiKey = process.env.REACT_APP_GRAPHHOPPER_API_KEY;

  useEffect(() => {
    if (points.length < 2 || points.length > 5) {
      setError(
        'Кол-во точек маршрута должно быть не менее двух и не более пяти',
      );
      return;
    }

    (async () => {
      const waypoints = points
        .map(
          (point) =>
            `&point=${(point as [number, number])[0]},${(point as [number, number])[1]}`,
        )
        .join('');

      const url =
        `${process.env.REACT_APP_GRAPHHOPPER_API_URL}/1/route?` +
        `${waypoints.substring(1, waypoints.length)}` +
        `&points_encoded=false` +
        `&vehicle=car` +
        `&key=${apiKey}`;
      setLoading(true);

      try {
        const response = await fetch(url);
        const data = await response.json();
        const isPathsExist = data && data.paths && data.paths.length > 0;
        const isCoordinatesExist =
          data.paths[0].points && data.paths[0].points.coordinates;
        if (isPathsExist && isCoordinatesExist) {
          const coordinates = data.paths[0].points.coordinates;
          const routeCoordinates = coordinates.map(
            (coordinate: [number, number]) => [coordinate[1], coordinate[0]],
          );
          setRoute(routeCoordinates);
        } else {
          setError(
            `Маршрут не найден или неправильная структура запроса: ${data.paths[0].points}`,
          );
        }
      } catch (error) {
        setError(`Ошибка получения маршрута: ${error}`);
      } finally {
        setLoading(false);
      }
    })();
  }, [points, apiKey]);

  return { route, error, loading };
};
