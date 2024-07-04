# GeoJSON Library for .NET

This library provides a set of classes for working with GeoJSON data in .NET applications. It includes converters for serializing and deserializing GeoJSON objects with the `System.Text.Json` library.

Most of the code is adapted from the [Microsoft Azure.Core library](https://github.com/Azure/azure-sdk-for-net) with some modifications to better align with specific use cases. These modifications address discrepancies in the interpretation of the GeoJSON specification ([RFC 7946](https://datatracker.ietf.org/doc/html/rfc7946)) and add features not present in the original library. Changes to improve code quality are also included.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Original License

The original code from the `Azure.Core` library is licensed under the MIT License - see the [LICENSE](https://github.com/Azure/azure-sdk-for-net/blob/main/LICENSE.txt) file for details.
