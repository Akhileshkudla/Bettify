import { Link } from "react-router-dom";
import { Button, Header, Icon, Segment } from "semantic-ui-react";

export default function NotFound() {
    return (
        <Segment placeholder>
            <Header icon>
                <Icon name='search' />
                Bruh! Whatever you are trying to search is not here! May be you can try searching in ****hub.com!
            </Header>
            <Segment.Inline>
                <Button as={Link} to='/activities'>
                    Go back to activies page 😜
                </Button>
            </Segment.Inline>
        </Segment>
    )
}