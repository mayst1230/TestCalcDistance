import { Button, Form, InputNumber } from "antd";
import { useEffect, useState } from "react";
import {
  GetDistanceQueryDefinition,
  GetDistanceQueryResult,
} from "../../services/openapi";
import { getDistance } from "../../services/map/ApiClient";
import { Map } from "../Map/Map";

interface FormGroup {
  firstLon: number;
  firstLat: number;
  secondLon: number;
  secondLat: number;
}

export function MapContainer() {
  const [submittable, setSubmittable] = useState(false);
  const [submited, setSubmited] = useState(false);
  const [mapProps, setMapProps] = useState<GetDistanceQueryDefinition>();
  const [queryResult, setQueryResult] = useState<GetDistanceQueryResult>();
  const [readyPopup, setReadyPopup] = useState<boolean>(false);
  const [mapZoom, setMapZoom] = useState<number>(2);

  const [form] = Form.useForm<FormGroup>();
  const formValues = Form.useWatch([], form);

  useEffect(() => {
    form.validateFields({ validateOnly: true }).then(
      () => {
        setSubmittable(true);
      },
      () => {
        setSubmittable(false);
      }
    );
  }, [form, formValues]);

  const onSubmit = () => {
    setSubmited(true);

    const request: GetDistanceQueryDefinition = {
      FirstMarker: {
        Lon: formValues.firstLon,
        Lat: formValues.firstLat,
      },
      SecondMarker: {
        Lat: formValues.secondLat,
        Lon: formValues.secondLon,
      },
    };

    setMapProps(request);
    getDistance(request).then((result) => {
      setQueryResult(result);
    });
  }

  const onReset = () => {
    setMapProps(undefined);
    setQueryResult(undefined);
    setSubmited(false);
    setReadyPopup(false);
    setMapZoom(2);
    form.resetFields();
  }

  return (
    <div className="md:container md:mx-auto px-4 m-5">
      <Form form={form}>
        <div className="grid grid-cols-2 gap-x-2">
          <div className="p-2">
            <div className="grid grid-rows-2 gap-y-2">
              <p className="font-semibold text-base">Первый маркер</p>
              <Form.Item
                name="firstLon"
                label="Широта"
                rules={[{ required: true, message: "Введите широту" }]}
              >
                <InputNumber
                  disabled={submited}
                  style={{ width: "100%" }}
                  placeholder="Введите широту"
                  step="0.00001"
                />
              </Form.Item>
              <Form.Item
                name="firstLat"
                label="Долгота"
                rules={[{ required: true, message: "Введите долготу" }]}
              >
                <InputNumber
                  disabled={submited}
                  style={{ width: "100%" }}
                  placeholder="Введите долготу"
                  step="0.00001"
                />
              </Form.Item>
            </div>
          </div>
          <div className="p-2">
            <div className="grid grid-rows-2 gap-y-2">
              <p className="font-semibold text-base">Второй маркер</p>
              <Form.Item
                name="secondLon"
                label="Широта"
                rules={[{ required: true, message: "Введите широту" }]}
              >
                <InputNumber
                  disabled={submited}
                  style={{ width: "100%" }}
                  placeholder="Введите широту"
                  step="0.00001"
                />
              </Form.Item>
              <Form.Item
                name="secondLat"
                label="Долгота"
                rules={[{ required: true, message: "Введите долготу" }]}
              >
                <InputNumber
                  disabled={submited}
                  style={{ width: "100%" }}
                  placeholder="Введите долготу"
                  step="0.00001"
                />
              </Form.Item>
            </div>
          </div>
          <Form.Item>
            <Button
              block={true}
              htmlType="submit"
              disabled={!submittable || submited}
              onClick={onSubmit}
            >
              Вычислить расстояние
            </Button>
          </Form.Item>
          <Form.Item>
            <Button block={true} htmlType="reset" onClick={onReset}>
              Сбросить
            </Button>
          </Form.Item>
        </div>
      </Form>
      <Map zoom={mapZoom} markers={mapProps} dist={queryResult?.Dist} onPopupReady={() => setReadyPopup(true)} readyPopup={readyPopup}></Map>
    </div>
  );
}
