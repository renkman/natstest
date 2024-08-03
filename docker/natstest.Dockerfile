FROM golang:1.22.2-bookworm as build

WORKDIR /src

COPY ../src/go ./

RUN go build ./cmd/natstest

FROM gcr.io/distroless/base-debian12 as app

WORKDIR /app
COPY --from=build /src/natstest .

USER nonroot:nonroot

ENTRYPOINT [ "/app/natstest" ]
