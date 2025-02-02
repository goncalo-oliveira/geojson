# Release History

## 1.1.0 (unreleased)

### Features Added

### Breaking Changes

### Bug Fixes

### Other Changes

## 1.0.2

### Breaking Changes

- Deserialization of invalid GeoJSON objects now returns `null` instead of throwing an exception. This can still be overridden by setting `GeoJsonSerializer.ThrowOnDeserializationError` to `true`.

## 1.0.1

### Bug Fixes

- Feature deserialization fails if id is a number #1

## 1.0.0

### Features Added

- Support for `Feature`<sup>[3.2](https://datatracker.ietf.org/doc/html/rfc7946#section-3.2)</sup> and `FeatureCollection`<sup>[3.3](https://datatracker.ietf.org/doc/html/rfc7946#section-3.3)</sup> objects.
- Extension method to calculate an object's bounding box.
- Parsing `GeoObject` from JSON stream or byte array.

### Bug Fixes

- Fixed `bbox` deserialization issue with `null` values.
