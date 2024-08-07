package main

import (
	"bytes"
	"context"
	"fmt"
	"log"
	"os"

	"github.com/nats-io/nats.go"
)

const subject string = "message"

var (
	buf    bytes.Buffer
	logger *log.Logger
)

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
	logger = log.New(&buf, "", log.Ldate|log.Ltime)

	natsUrl := initApp()
	logInfo("Connect to nats: %s\n", string(natsUrl))
	nc, err := nats.Connect(natsUrl)
	if err != nil {
		return err
	}

	defer nc.Drain()

	nc.Subscribe(subject, func(msg *nats.Msg) {
		logInfo("Recieved message: %s\n", string(msg.Data))
	})

	for {
		select {
		case <-ctx.Done():
			return nil
		}
	}
}

func logInfo(format string, params ...any) {
	logger.Printf(format, params)
	fmt.Print(&buf)
	buf.Reset()
}
