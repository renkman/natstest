package main

import (
	"fmt"
	"os"
	"time"

	"github.com/nats-io/nats.go"
)

func main() {
	nc, err := nats.Connect(nats.DefaultURL)
	if err != nil {
		fmt.Printf("Error: Cannot connect to NATS: %s\n", err.Error())
		os.Exit(1)
	}
	defer nc.Drain()

	nc.Subscribe("test", func(msg *nats.Msg) {
		fmt.Printf("Recieved message: %s\n", string(msg.Data))
	})

	send(nc)
}

func send(nc *nats.Conn) {
	start := time.Now()
	for time.Since(start) <= time.Duration(10*time.Second) {
		time.Sleep(time.Duration(time.Second))
		nc.Publish("test", []byte("foobar"))
	}
}
