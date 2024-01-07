import {
  MapContainer,
  Marker,
  Polyline,
  Popup,
  TileLayer,
} from "react-leaflet";
import "leaflet/dist/leaflet.css";
import "../Map/Map.css";
import { Icon } from "leaflet";
import { useEffect, useRef, useState } from "react";
import { GetDistanceQueryDefinition } from "../../services/openapi";
import { MapChangeView } from "../MapChangeView/MapChangeView";

interface Props {
  markers?: GetDistanceQueryDefinition;
  dist?: number;
  zoom: number;
  readyPopup: boolean;
  onPopupReady: () => void;
}

export function Map(props: Props) {
  const [bounds, setBounds] = useState<number[][]>();
  const markerRefs = useRef({}) as any;

  useEffect(() => {
    if (
      props.markers?.FirstMarker?.Lat &&
      props.markers?.FirstMarker?.Lon &&
      props.markers?.SecondMarker?.Lat &&
      props.markers?.SecondMarker?.Lon
    ) {
      setBounds([
        [props.markers.FirstMarker.Lon, props.markers.FirstMarker?.Lat],
        [props.markers.SecondMarker.Lon, props.markers.SecondMarker?.Lat],
      ]);
    } else {
      setBounds(undefined);
    }
  }, [
    props.markers?.FirstMarker?.Lat,
    props.markers?.FirstMarker?.Lon,
    props.markers?.SecondMarker?.Lat,
    props.markers?.SecondMarker?.Lon,
  ]);

  useEffect(() => {
    if (props.readyPopup) {
      const markerToOpen = markerRefs.current;
      markerToOpen?.openPopup();
    }
  }, [props.readyPopup]);

  const customIcon = new Icon({
    iconUrl: "https://cdn-icons-png.flaticon.com/512/684/684908.png",
    iconSize: [38, 38],
  });

  return (
    <>
      <MapContainer
        worldCopyJump={true}
        center={[51.505, -0.09]}
        zoom={props.zoom}
        scrollWheelZoom={true}
        bounds={bounds as L.LatLngBoundsExpression}
        attributionControl={false}
      >
        {props.markers?.FirstMarker?.Lat &&
          props.markers?.FirstMarker?.Lon &&
          props.markers?.SecondMarker?.Lon &&
          props.markers?.SecondMarker?.Lat && (
            <MapChangeView
              markers={[
                {
                  Lat: props.markers.FirstMarker.Lat,
                  Lon: props.markers.FirstMarker.Lon,
                },
                {
                  Lat: props.markers.SecondMarker.Lat,
                  Lon: props.markers.SecondMarker.Lon,
                },
              ]}
            />
          )}

        <TileLayer
          attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
          url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        />
        {bounds?.map((point, index) => (
          <Marker
            key={index}
            position={point as L.LatLngExpression}
            icon={customIcon}
          >
          </Marker>
        ))}
        {props.markers?.FirstMarker?.Lat &&
          props.markers?.FirstMarker.Lon &&
          props.markers?.SecondMarker?.Lat &&
          props.markers?.SecondMarker.Lon &&
          props.dist && (
            <Polyline
              ref={(ref) => {
                markerRefs.current = ref;
                if (ref !== null)
                  props.onPopupReady();
              }}
              positions={[
                [props.markers.FirstMarker.Lon, props.markers.FirstMarker.Lat],
                [
                  props.markers.SecondMarker.Lon,
                  props.markers.SecondMarker.Lat
                ],
              ]}
              color={"red"}
            >
              <Popup>{props.dist} ml</Popup>
            </Polyline>
          )}
      </MapContainer>
    </>
  );
}
