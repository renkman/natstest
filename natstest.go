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
	for time.Until(start) <= time.Duration(time.Duration.Seconds(10)) {
		time.Sleep(time.Duration(time.Duration.Seconds(1)))
		nc.Publish("test", []byte("foobar"))
	}
}
