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

func initApp() string {
	natsUrl, ok := os.LookupEnv("NATS_URL")
	if ok {
		return natsUrl
	}
	return nats.DefaultURL
}

func run(ctx context.Context) error {
	natsUrl := initApp()
	fmt.Printf("Connect to nats: %s\n", string(natsUrl))

	nc, err := nats.Connect(natsUrl)
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
