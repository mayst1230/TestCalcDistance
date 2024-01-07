import { useMap } from "react-leaflet";
import { GeoCord } from "../../services/openapi";
import { latLngBounds } from "leaflet";

interface Props {
  markers: GeoCord[];
}

export function MapChangeView(props: Props) {
  const map = useMap();
  let markerBounds = latLngBounds([]);
  props.markers.forEach((marker) => {
    markerBounds.extend([marker.Lon, marker.Lat]);
  });
  
  map.fitBounds(markerBounds);

  return null;
}
