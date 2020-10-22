OctonautsCli is a utility to manipulate Octopus projects, release, channels on a project group basis as well as some other useful commands.

You can use dotnet tool to install it:

```
dotnet tool update -g OctonautsCli
```

Then run it via command line 

```
Octonauts --help
```

Or use choco

```
choco upgrade OctonautsCli
```

then run it via command line

```
OctonautsCli --help
```

```
This tool can be executed with arguments like:
        --feature <feature name> --command <command of feature> <arguments for the command>
For example to find a machine by thumbprint:
        --feature machine --command find-by-thumbprint --thumbprint CAB8994D6B919C62E7B747FB

For each feature/command, you can use help to find out the required arguments such as:
        '--feature release help' to see the commands of the release feature
        '--feature release --command create help' to see the required args to create a release

To avoid entering the Server Url and APIKey every time, you can define them as environment variables
        OCTOPUS_SERVERURL for the server url
        OCTOPUS_APIKEY for the API key
Supported Features:
        release
                Supported commands are:
                help:   Help
                create: Create a release for project(s) or project group
                delete: Delete a release from project(s) or project group
                delete-by-range:        Batch delete releases by version range
                update-variables:       Update variable snapshot for a release for project(s) or project group
                promote-to-channel:     Promote a release to another channel for project(s) or project group

        channel
                Supported commands are:
                help:   shows the available commands
                create: Create a channel
                delete: Delete a channel

        package
                Supported commands are:
                help:   Help
                get-used:       Get packages used by project(s) or project group

        environment
                Supported commands are:
                help:   Help
                delete: Delete environments that matches regex pattern

        machine
                Supported commands are:
                help:   Help
                deploy-project: Individually deploy a project to machines in an environment
                find-by-thumbprint:     Find a machine by its thumbprint
                list-machines:  list machines in an environment
                set-roles:      set roles of the machine

        help
                show above help message
```
