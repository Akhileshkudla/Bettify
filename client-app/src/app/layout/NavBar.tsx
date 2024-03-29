import { Button, Container, Menu, Image, Dropdown, Icon } from 'semantic-ui-react';
import { Link, NavLink } from 'react-router-dom';
import { useStore } from '../stores/store';
import { observer } from 'mobx-react-lite';
import { useEffect } from 'react';
import ChangePassword from './ChangePassword';

export default observer(function NavBar() {
    const {modalStore, userStore: {user, logout, users, getallusers} } = useStore();

    const totalAmount = users.reduce((total, user) => total + user.amount, 0);

    useEffect(() => {
        // Fetch users when component mounts
        getallusers();
    }, [getallusers]);

    return (
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item as={NavLink} to='/' header>
                    <img src='/assets/logo2.png' alt='logo' style={{marginRight: 10}} />
                    Bettify
                </Menu.Item>
                <Menu.Item as={NavLink} to='/activities' name='Actvities' icon='gamepad' />
                {/* <Menu.Item as={NavLink} to='/errors' name='Errors' />                 */}
                {user!== null && user!.username === 'admin' && (
                    <Menu.Item as={NavLink} to='/users' name='Users' />
                )}   
                <Menu.Item as={NavLink} to='/leaderboard' name='Leaderboard' icon='trophy' />       
                {user?.username === 'admin' &&
                <Menu.Item>                
                    <Button                     
                        as={NavLink} 
                        to='/createActivity' 
                        positive 
                        content='Create Activity'
                        disabled={user?.username !== 'admin'}
                    />  
                </Menu.Item>
                }
                <Menu.Item position='right'>
                    <Icon name='inr'></Icon>
                    Party Fund Total: ₹{totalAmount} 
                </Menu.Item>
                <Menu.Item position='right'>
                    <Image src={user?.image || '/assets/user.png'} avatar spaced='right' />
                    <Dropdown pointing='top left' text={user?.displayName} >
                        <Dropdown.Menu>
                            <Dropdown.Item as={Link} to={`/profile/${user?.username}`} text="My Profile" icon='user' />
                            <Dropdown.Item onClick={ () => modalStore.openModal(<ChangePassword />)} text="Change Password" icon='key' />
                            <Dropdown.Item as={NavLink} to='/transcations' text="Transactions" icon='send' />
                            <Dropdown.Item onClick={logout} text='Logout' icon='power' />
                        </Dropdown.Menu>
                    </Dropdown>
                </Menu.Item>
            </Container>
        </Menu>
    )
})