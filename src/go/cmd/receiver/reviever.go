package main

import (
	"context"
	"fmt"
	"os"

	"github.com/nats-io/nats.go"
)

const subject string = "message"

func main() {
	ctx := context.Background()
	ctx, cancel := context.WithCancel(ctx)

	defer func() {
		cancel()
	}()

	err := run(ctx)
	if err != nil {
		fmt.Fprintf(os.Stderr, "%s\n", err)
		os.Exit(1)
	}
}

func run(ctx context.Context) error {
	nc, err := nats.Connect(nats.DefaultURL)
	if err != nil {
		return err
	}

	defer nc.Drain()

	nc.Subscribe(subject, func(msg *nats.Msg) {
		fmt.Printf("Recieved message: %s\n", string(msg.Data))
	})

	for {
		select {
		case <-ctx.Done():
			return nil
		}
	}
}
